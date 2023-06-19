using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models.InputModels
{
    public class UserRegister
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Nickname { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

    }
}
