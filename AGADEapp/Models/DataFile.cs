using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models
{
    public class DataFile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string FileType { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public string Author { get; set; }

        public FileStatus Status { get; set; }

    }
}
