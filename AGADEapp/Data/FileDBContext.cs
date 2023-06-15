using AGADEapp.Models;
using Microsoft.EntityFrameworkCore;

namespace AGADEapp.Data.Configration
{
    public class FileDBContext: DbContext
    {
        public FileDBContext(DbContextOptions<FileDBContext> options): base(options)
        {

        }

        public DbSet<DataFile> DataFile { get; set; }
    }
}
