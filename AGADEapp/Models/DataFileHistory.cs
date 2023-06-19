using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models
{
    public class DataFileHistory
    {
        [Required]
        public int Id { get; set; }

        // Zadeklarowana relacja do DataFile
        [Required]
        public int DataFileId { get; set; } // Klucz obcy

        // Zadeklarowana relacja do History element
        public virtual ICollection<HistoryElement> Actions { get; set; } // Relacja jeden do wielu

        public DataFileHistory()
        {
            Actions = new List<HistoryElement>();
        }
    }
}
