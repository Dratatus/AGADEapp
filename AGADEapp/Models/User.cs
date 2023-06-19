using AGADEapp.Models.InputModels;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AGADEapp.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        // Zadeklarowana relacja do UserData
        public virtual UserData? UserData { get; set; } // Referencja do UserData

        public static User of(UserRegister dto)
        {
            if (dto is null)
            {
                return null;
            }
            return new User()
            {
                Id = 0,
                Login = dto.Login,
                Password = dto.Password,
                UserData = null
            };
        }
    }
}
