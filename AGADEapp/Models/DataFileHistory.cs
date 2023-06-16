using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models
{
    public class DataFileHistory
    {
        [Required]
        public int Id { get; set; }
        [Required]

        // Zadeklarowana relacja do DataFile
        public int DataFileId { get; set; } // Klucz obcy
        public DataFile DataFile { get; set; } = null!; // Referencja do DataFile

        // Zadeklarowana relacja do History element
        public virtual ICollection<HistoryElement> Actions { get; set; } // Relacja jeden do wielu

        public DataFileHistory()
        {
            Actions = new List<HistoryElement>();
        }
    }
}
