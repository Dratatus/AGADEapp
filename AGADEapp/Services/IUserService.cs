using AGADEapp.Models;

namespace AGADEapp.Services
{
    public interface IUserService
    {
        User Register(string login, string password, string nickname, string name, string surname);
        User Login(string login, string password);
        void RemoveUser(int id);
        void Logout();
    }
}
