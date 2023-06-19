using AGADEapp.Models;
using AGADEapp.Models.InputModels;

namespace AGADEapp.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> Register(UserRegister user);
        Task<User> Login(string login, string password);
        Task RemoveUser(int id);
        Task Logout();

        Task<bool> IsAdmin(int? id);

        Task<string> GetUserName(int? id);
    }
}
