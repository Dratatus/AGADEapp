using AGADEapp.Data.Configration;
using AGADEapp.Models;
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
        public User Login(string login, string password)
        {
            var user = _userDBcontext.User.FirstOrDefault(m => m.Login == login);
            if (user.Password == password)
            {
                return user;
            }
            throw new Exception("Invalid login or password");
        }

        //Nie mam pojęcia jak ma działać logout w tym wypadku
        public void Logout()
        {
            throw new NotImplementedException();
        }

        //Tworzy nowego użytkownika oraz zwraca obiekt
        public User Register(string login, string password, string nickname, string name, string surname)
        {
            User newUser = new User()
            {
                Login = login,
                Password = password,
                UserData = new UserData()
                {
                    IsAdmin = false,
                    Nickname = nickname,
                    Name = name,
                    Surname = surname
                }
            };
            _userDBcontext.User.AddAsync(newUser);
            _userDBcontext.SaveChangesAsync();

            return newUser;
        }

        //Nie wiem czy usunięcie Usera automatycznie usuwa UserData, trzeba przetestować
        //Usuwa użytkownika po id
        public void RemoveUser(int id)
        {
            var userToDelete = _userDBcontext.User.Find(id);
            if (userToDelete is not null)
            {
                _userDBcontext.User.Remove(userToDelete);
                _userDBcontext.SaveChanges();
            }
        }
    }
}
