using AGADEapp.Models;
using AGADEapp.Models.InputModels;

namespace AGADEapp.Services
{
    public interface IFileService
    {
        Task<List<DataFile>> GetAllFiles();

        Task<List<DataFile>> GetFiles(bool? isAdmin, string? username);

        Task<List<HistoryElement>>? GetFileHistory(int fileId);

        Task<List<DataFile>> GetMyFiles(string username);

        Task<DataFile?> GetFileById(int id);

        Task<DataFile> CreateFile(DataFile file);

        Task<DataFile?> UpdateFile(int id, DataFile file, string username);

        Task<DataFile?> Upload(string username, UploadFile obj, int fileId);

        Task ConfirmDownload(string username, int fileId);

        Task RemoveAttachment(string username, int fileId);

        Task<bool> IsOwner(string? username, int fileId);

        Task DeleteFile(int id);
    }
}
