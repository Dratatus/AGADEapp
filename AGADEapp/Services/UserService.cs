using AGADEapp.Data.Configration;
using AGADEapp.Models;
using AGADEapp.Models.InputModels;
using Microsoft.EntityFrameworkCore;

namespace AGADEapp.Services
{
    public class UserService : IUserService
    {
        private readonly UserDBContext _userDBcontext;

        public UserService(UserDBContext dBContext)
        {
            _userDBcontext = dBContext;
        }

        //Zwraca dane zalogowanego użytkownika po sprawdzeniu loginu i hasła
        public async Task<User> Login(string login, string password)
        {
            var user = _userDBcontext.User.Include(a => a.UserData).FirstOrDefault(m => m.Login == login);
            if (user.Password == password)
            {
                return user;
            }
            throw new Exception("Invalid login or password");
        }

        //Zwraca informację o poprawnym wylogowaniu
        public Task Logout()
        {
            throw new Exception();
        }

        //Tworzy nowego użytkownika oraz zwraca obiekt User
        public async Task<User> Register(UserRegister user)
        {
            User newUser = User.of(user);
            User userToAdd = new User()
            {
                Login = newUser.Login,
                Password = newUser.Password,
                UserData = new UserData()
                {
                    IsAdmin = false,
                    Nickname = user.Nickname,
                    Name = user.Name,
                    Surname = user.Surname
                }
            };
            await _userDBcontext.User.AddAsync(userToAdd);
            await _userDBcontext.SaveChangesAsync();

            return newUser;
        }

        //Usuwa użytkownika po podanym id
        public async Task RemoveUser(int id)
        {
            var userToDelete = await _userDBcontext.User.FindAsync(id);
            if (userToDelete is not null)
            {
                _userDBcontext.User.Remove(userToDelete);
                _userDBcontext.SaveChanges();
            }
        }

        //Zwraca listę wszystkich użytkowników
        public async Task<List<User>> GetAllUsers()
        {
            return _userDBcontext.User.Include(a => a.UserData).ToList();
        }

        //Zwraca informację czy dany użytkownik ma prawa administratora
        public async Task<bool> IsAdmin(int? id)
        {
            if(id == null) return false;
            var isAdmin = _userDBcontext.User.Include(a => a.UserData).FirstOrDefault(m => m.Id == id).UserData.IsAdmin;
            return isAdmin;
        }

        //Zwraca login danego użytkownika na podstawie podanego id
        public async Task<string?> GetUserName(int? id)
        {
            if (id == null) return null;
            var username = _userDBcontext.User.FirstOrDefault(m => m.Id == id).Login;
            return username;
        }
    }
}
