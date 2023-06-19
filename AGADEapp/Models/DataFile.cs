using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models
{
    public class DataFile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string? ContentType { get; set; }

        public string? Content { get; set; }

        [Required]
        public string? Author { get; set; }

        public FileStatus Status { get; set; }

        // Zadeklarowana relacja do DataFileHistory

        public virtual DataFileHistory? DataFileHistory { get; set; } // Referencja do DataFileHistory
        //public DataFileHistory? DataFileHistory { get; set; } // Referencja do DataFileHistory

    }
}
