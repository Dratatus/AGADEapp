using AGADEapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AGADEapp.Data.Configration
{
    public class FileDBContext : DbContext
    {
        public FileDBContext(DbContextOptions<FileDBContext> options) : base(options)
        {

        }

        public DbSet<DataFile> DataFile { get; set; }
        public DbSet<DataFileHistory> DataFileHistory { get; set; }
        public DbSet<HistoryElement> HistoryElement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataFile>().HasData(
                new DataFile() { Id = 1, Title = "Test1", ContentType = "application/pdf", Content = "TEST 1.pdf", Author = "test1@fake.fake", Status = FileStatus.Public },
                new DataFile() { Id = 2, Title = "Test2", ContentType = "image/png", Content = "test1.png", Author = "admin1@fake.fake", Status = FileStatus.Private },
                new DataFile() { Id = 3, Title = "Test3", ContentType = "application/pdf", Content = "TEST 2.pdf", Author = "test2@fake.fake", Status = FileStatus.Confidential },
                new DataFile() { Id = 4, Title = "Test4", ContentType = "image/png", Content = "test2.png", Author = null, Status = FileStatus.Public },
                new DataFile() { Id = 5, Title = "Test5", ContentType = null, Content = null, Author = null, Status = FileStatus.Public },
                new DataFile() { Id = 6, Title = "Test6", ContentType = null, Content = null, Author = "test1@fake.fake", Status = FileStatus.Private }
            );

            modelBuilder.Entity<DataFileHistory>().HasData(
                new DataFileHistory() { Id = 1, DataFileId = 1, Actions = new List<HistoryElement>() },
                new DataFileHistory() { Id = 2, DataFileId = 2, Actions = new List<HistoryElement>() },
                new DataFileHistory() { Id = 3, DataFileId = 3, Actions = new List<HistoryElement>() },
                new DataFileHistory() { Id = 4, DataFileId = 4, Actions = new List<HistoryElement>() },
                new DataFileHistory() { Id = 5, DataFileId = 5, Actions = new List<HistoryElement>() },
                new DataFileHistory() { Id = 6, DataFileId = 6, Actions = new List<HistoryElement>() }
            );

            modelBuilder.Entity<HistoryElement>().HasData(
                new HistoryElement() { Id = 1, Action = OperationType.Create, User = "test1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 1 },
                new HistoryElement() { Id = 2, Action = OperationType.Upload, User = "test1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 1 },
                new HistoryElement() { Id = 3, Action = OperationType.Download, User = null, OperationDate = DateTime.Now, DataFileHistoryId = 1 },
                new HistoryElement() { Id = 4, Action = OperationType.Download, User = null, OperationDate = DateTime.Now, DataFileHistoryId = 1 },

                new HistoryElement() { Id = 5, Action = OperationType.Create, User = "admin1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 2 },
                new HistoryElement() { Id = 6, Action = OperationType.Update, User = "admin1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 2 },
                new HistoryElement() { Id = 7, Action = OperationType.Upload, User = "test2@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 2 },
                new HistoryElement() { Id = 8, Action = OperationType.Upload, User = "admin1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 2 },

                new HistoryElement() { Id = 9, Action = OperationType.Create, User = "test2@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 3 },
                new HistoryElement() { Id = 10, Action = OperationType.Upload, User = "test2@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 3 },
                new HistoryElement() { Id = 11, Action = OperationType.Download, User = "test2@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 3 },

                new HistoryElement() { Id = 12, Action = OperationType.Create, User = null, OperationDate = DateTime.Now, DataFileHistoryId = 4 },
                new HistoryElement() { Id = 13, Action = OperationType.Upload, User = null, OperationDate = DateTime.Now, DataFileHistoryId = 4 },

                new HistoryElement() { Id = 14, Action = OperationType.Create, User = null, OperationDate = DateTime.Now, DataFileHistoryId = 5 },
                new HistoryElement() { Id = 15, Action = OperationType.Upload, User = "test2@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 5 },
                new HistoryElement() { Id = 16, Action = OperationType.RemoveAttachment, User = "admin1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 5 },

                new HistoryElement() { Id = 17, Action = OperationType.Create, User = "test1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 6 },
                new HistoryElement() { Id = 18, Action = OperationType.Update, User = "admin1@fake.fake", OperationDate = DateTime.Now, DataFileHistoryId = 6 }
            );
        }
    }
}
