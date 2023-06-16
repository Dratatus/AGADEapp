using AGADEapp.Models;

namespace AGADEapp.Services
{
    public interface IUserService
    {
        User Register();
        User Login();
        void RemoveUser();
        void Logout();
    }
}
