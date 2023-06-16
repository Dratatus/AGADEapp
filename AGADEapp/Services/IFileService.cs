using AGADEapp.Models;

namespace AGADEapp.Services
{
    public interface IFileService
    {
        Task<List<DataFile>> GetAllFiles();

        Task<DataFile?> GetFileById(int id);

        Task<DataFile> CreateFile(DataFile file);

        Task<DataFile?> UpdateFile(int id, DataFile file);

        Task DeleteFile(int id);
    }
}
