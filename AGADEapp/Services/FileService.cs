using AGADEapp.Data.Configration;
using AGADEapp.Models;
using AGADEapp.Models.InputModels;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace AGADEapp.Services
{
    public class FileService : IFileService
    {
        private readonly FileDBContext _fileDBcontext;
        public FileService(FileDBContext dBContext)
        {
            _fileDBcontext = dBContext;
        }

        //Dodaje plik do bazy danych
        public async Task<DataFile> CreateFile(DataFile file)
        {
            file.DataFileHistory = new DataFileHistory
            {
                DataFileId = file.Id
            };

            file.DataFileHistory.Actions.Add(new HistoryElement { Action = OperationType.Create, User = file.Author });
            await _fileDBcontext.DataFile.AddAsync(file);
            await _fileDBcontext.SaveChangesAsync();
            return file;
        }

        //Usuwa plik z bazy
        public async Task DeleteFile(int id)
        {
            var dataFileToDelete = await _fileDBcontext.DataFile.FindAsync(id);
            if (dataFileToDelete is not null)
            {
                _fileDBcontext.DataFile.Remove(dataFileToDelete);
                _fileDBcontext.SaveChanges();
            }
        }

        //Zwraca wszystkie pliki
        public async Task<List<DataFile>> GetAllFiles()
        {
            return  _fileDBcontext.DataFile.Include(a => a.DataFileHistory).ToList();
        }

        //Zwraca wszystkie upoważnione pliki na podstawie użytkownika
        public async Task<List<DataFile>> GetFiles(bool? isAdmin, string? username)
        {
            if(isAdmin == null)
            {
                return _fileDBcontext.DataFile.Include(a => a.DataFileHistory).Where(a => a.Status == FileStatus.Public).ToList();
            }
            else if ((bool)isAdmin)
            {
                return await GetAllFiles();
            }
            return _fileDBcontext.DataFile.Include(a => a.DataFileHistory).Where(a => a.Author == username || a.Status != FileStatus.Confidential).ToList();
        }

        //Zwraca pliki danego użytkownika
        public async Task<List<DataFile>> GetMyFiles(string username)
        {
            return _fileDBcontext.DataFile.Include(a => a.DataFileHistory).Where(a => a.Author == username).ToList();
        }

        //Zwraca konkretny plik po id
        public async Task<DataFile> GetFileById(int id)
        {
            var file = _fileDBcontext.DataFile.Include(a => a.DataFileHistory).FirstOrDefault(m => m.Id == id);
            return file ?? null;
        }

        //Aktualizuje konkretny plik po id, danymi z załączonego DataFile
        public async Task<DataFile> UpdateFile(int id, DataFile file, string username)
        {
            var fileToEdit = _fileDBcontext.DataFile.Include(a => a.DataFileHistory).FirstOrDefault(a => a.Id == id);

            fileToEdit.Title = file.Title;
            fileToEdit.Status = file.Status;

            fileToEdit.DataFileHistory.Actions.Add(new HistoryElement { Action = OperationType.Update, User = username });

            await _fileDBcontext.SaveChangesAsync();

            return fileToEdit;
        }

        //Wprowadza dane wysłanego załącznika do odpowiedniego DataFile
        public async Task<DataFile> Upload(string username, UploadFile obj, int fileId)
        {
            var edit = await GetFileById(fileId);
            edit.Content = obj.file.FileName;
            edit.ContentType = obj.file.ContentType;
            await UpdateFile(fileId, edit, username);

            edit.DataFileHistory.Actions.Add(new HistoryElement { Action = OperationType.Upload, User = username });
            await _fileDBcontext.SaveChangesAsync();

            return edit;
        }

        //Wprowadza wpis do historii działań na pliku po udanym pobraniu załącznika
        public async Task ConfirmDownload(string username, int fileId)
        {
            var edit = await GetFileById(fileId);
            edit.DataFileHistory.Actions.Add(new HistoryElement { Action = OperationType.Download, User = username });
            await _fileDBcontext.SaveChangesAsync();
        }

        //Usuwa załącznik danego DataFile
        public async Task RemoveAttachment(string username, int fileId)
        {
            var edit = await GetFileById(fileId);
            edit.Content = null;
            edit.ContentType = null;
            edit.DataFileHistory.Actions.Add(new HistoryElement { Action = OperationType.RemoveAttachment, User = username });
            await _fileDBcontext.SaveChangesAsync();
        }

        //Zwraca informacje czy dany użytkownik jest właścicielem pliku
        public async Task<bool> IsOwner(string? username, int fileId)
        {
            if (username == null) return false;
            var file = await GetFileById(fileId);
            if (file.Author == username)
            {
                return true;
            }
            return false;
        }

        //Zwraca historie działań na danym pliku
        public async Task<List<HistoryElement>>? GetFileHistory(int fileId)
        {
            var actions = _fileDBcontext.DataFileHistory.Include(a => a.Actions).FirstOrDefault(a => a.DataFileId == fileId).Actions.ToList();
            return actions;
        }
    }
}
