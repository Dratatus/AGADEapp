using AGADEapp.Models;
using AGADEapp.Models.InputModels;

namespace AGADEapp.Services
{
    public interface IFileService
    {
        Task<List<DataFile>> GetAllFiles();

        Task<List<DataFile>> GetFiles(bool? isAdmin, string? username);

        Task<List<DataFile>> GetMyFiles(string username);

        Task<DataFile?> GetFileById(int id);

        Task<DataFile> CreateFile(DataFile file);

        Task<DataFile?> UpdateFile(int id, DataFile file);

        Task<DataFile?> Upload(string username, UploadFile obj, int fileId);

        Task DeleteFile(int id);
    }
}
