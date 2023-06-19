using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models.InputModels
{
    public class DataFileInit
    {
        [Required]
        public string Title { get; set; }

        public string? Author { get; set; }

        public FileStatus Status { get; set; } = FileStatus.Public;
    }
}
