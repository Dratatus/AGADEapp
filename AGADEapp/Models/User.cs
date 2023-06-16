using System.Reflection.Metadata;

namespace AGADEapp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        // Zadeklarowana relacja do UserData
        public UserData? UserData { get; set; } // Referencja do UserData
    }
}
