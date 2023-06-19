using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models.InputModels
{
    public class UploadFile
    {
        [Required]
        public IFormFile file { get; set; }

        string comment { get; set; }
    }
}
