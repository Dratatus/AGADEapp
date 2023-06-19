using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models
{
    public class HistoryElement
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public OperationType Action { get; set; }

        public string? User { get; set; }

        public DateTime OperationDate { get; set; }

        // Zadeklarowana relacja do DataFileHistory
        public virtual int DataFileHistoryId { get; set; } // Klucz obcy

        public HistoryElement() 
        {
            OperationDate = DateTime.Now;
        }
    }
}
