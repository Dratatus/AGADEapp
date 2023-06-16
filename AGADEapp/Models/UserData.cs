using System.Reflection.Metadata;

namespace AGADEapp.Models
{
    public class UserData
    {
        public int Id { get; set; }

        public bool IsAdmin { get; set; }

        public string Nickname { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        // Zadeklarowana relacja do User
        public int UserId { get; set; } // Klucz obcy
        public User User { get; set; } = null!; // Referencja do User
    }
}
