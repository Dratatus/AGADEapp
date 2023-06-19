using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AGADEapp.Models
{
    public class UserData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public string Nickname { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        // Zadeklarowana relacja do User
        public virtual int UserId { get; set; } // Klucz obcy
    }
}
