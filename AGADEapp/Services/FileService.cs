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

        //Dodaje plik do bazy
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

        //Ustawiłem by nie zwracała niczego, więc albo trzeba jej używać w try catch albo zmodyfikowac by cos zwracała
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

        //Zwraca pliki na podstawie userId
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

        //Zwraca pliki na podstawie userId
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

        //Nie wiem jak to do końca działa ale powinno, jak z tym entityState.Modified dziala lepiej to można zmienić
        //Aktualizuje konkretny plik po id danymi z załączonego DataFile
        public async Task<DataFile> UpdateFile(int id, DataFile file)
        {
            var fileToEdit = await _fileDBcontext.DataFile.FindAsync(id);

            fileToEdit.Title = file.Title;
            fileToEdit.ContentType = file.ContentType;
            fileToEdit.Content = file.Content;
            fileToEdit.Author = file.Author;
            fileToEdit.Status = file.Status;

            await _fileDBcontext.SaveChangesAsync();

            return fileToEdit;
        }

        public async Task<DataFile> Upload(string username, UploadFile obj, int fileId)
        {
            var edit = await GetFileById(fileId);
            edit.Content = obj.file.FileName;
            edit.ContentType = obj.file.ContentType;
            await UpdateFile(fileId, edit);

            edit.DataFileHistory.Actions.Add(new HistoryElement { Action = OperationType.Upload, User = username });
            await _fileDBcontext.SaveChangesAsync();

            return edit;
        }
    }
}
