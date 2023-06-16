using AGADEapp.Data.Configration;
using AGADEapp.Models;
using Microsoft.EntityFrameworkCore;

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
        public DataFile CreateFile(DataFile file)
        {
            _fileDBcontext.DataFile.AddAsync(file);
            _fileDBcontext.SaveChangesAsync();
            return file;
        }

        //Ustawiłem by nie zwracała niczego, więc albo trzeba jej używać w try catch albo zmodyfikowac by cos zwracała
        //Usuwa plik z bazy
        public void DeleteFile(int id)
        {
            var dataFileToDelete = _fileDBcontext.DataFile.Find(id);
            if (dataFileToDelete is not null)
            {
                _fileDBcontext.DataFile.Remove(dataFileToDelete);
                _fileDBcontext.SaveChanges();
            }
        }

        //Zwraca wszystkie pliki
        public List<DataFile> GetAllFiles()
        {
            return _fileDBcontext.DataFile.ToList();
        }

        //Zwraca konkretny plik po id
        public DataFile GetFileById(int id)
        {
            var file = _fileDBcontext.DataFile.Include(a => a.DataFileHistory).FirstOrDefault(m => m.Id == id);
            return file ?? null;
        }

        //Nie wiem jak to do końca działa ale powinno, jak z tym entityState.Modified dziala lepiej to można zmienić
        //Aktualizuje konkretny plik po id danymi z załączonego DataFile
        public DataFile UpdateFile(int id, DataFile file)
        {
            var dataFileToUpdate = _fileDBcontext.DataFile.Find(id);

            if (dataFileToUpdate == null)
            {
                dataFileToUpdate.Title = file.Title;
                dataFileToUpdate.Status = file.Status;
                dataFileToUpdate.Author = file.Author;
                dataFileToUpdate.FileType = file.FileType;
                dataFileToUpdate.Content = file.Content;
                dataFileToUpdate.ContentType = file.ContentType;

                // Tutaj trzeba dodać dodanie wpisu do DataFileHistory


                _fileDBcontext.SaveChanges();
                return dataFileToUpdate;
            }
            return null;
        }
    }
}
