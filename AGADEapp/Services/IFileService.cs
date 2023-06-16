using AGADEapp.Models;

namespace AGADEapp.Services
{
    public interface IFileService
    {
        List<DataFile> GetAllFiles();

        DataFile GetFileById(int id);

        DataFile CreateFile(DataFile file);

        DataFile UpdateFile(int id, DataFile file);

        void DeleteFile(int id);
    }
}
