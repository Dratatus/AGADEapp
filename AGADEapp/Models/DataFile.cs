using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AGADEapp.Models.InputModels;

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

        public string? Author { get; set; }

        public FileStatus Status { get; set; }

        // Zadeklarowana relacja do DataFileHistory

        public virtual DataFileHistory? DataFileHistory { get; set; } // Referencja do DataFileHistory
        //public DataFileHistory? DataFileHistory { get; set; } // Referencja do DataFileHistory

        public static DataFile of(DataFileInit dto, string username)
        {
            if (dto is null)
            {
                return null;
            }
            return new DataFile()
            {
                Id = 0,
                Title = dto.Title,
                ContentType = null,
                Content = null,
                Author = username,
                Status = dto.Status,

                DataFileHistory = null
            };
        }
    }
}
