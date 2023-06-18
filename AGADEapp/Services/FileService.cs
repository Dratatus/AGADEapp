using AGADEapp.Data.Configration;
using AGADEapp.Models;
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
            return  _fileDBcontext.DataFile.ToList();
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
    }
}
