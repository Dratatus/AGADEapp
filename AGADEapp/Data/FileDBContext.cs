using AGADEapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AGADEapp.Data.Configration
{
    public class FileDBContext: DbContext
    {
        public FileDBContext(DbContextOptions<FileDBContext> options): base(options)
        {

        }

        public DbSet<DataFile> DataFile { get; set; }
        public DbSet<DataFileHistory> DataFileHistory { get; set; }
        public DbSet<HistoryElement> HistoryElement { get; set; }
    }
}
