using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models
{
    public class UploadFile
    {
        [Required]
        public int dataFileId { get; set; }

        [Required]
        public IFormFile file { get; set; }
    }
}
