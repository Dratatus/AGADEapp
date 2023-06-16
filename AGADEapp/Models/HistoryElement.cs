using System.ComponentModel.DataAnnotations;

namespace AGADEapp.Models
{
    public class HistoryElement
    {
        public OperationType Action { get; set; }

        public string User { get; set; }

        public DateTime OperationDate { get; set; }


        // Zadeklarowana relacja do DataFileHistory
        public int DataFileHistoryId { get; set; } // Klucz obcy
        public virtual DataFileHistory? DataFileHistory { get; set; } // Referencja do DataFileHistory
    }
}
