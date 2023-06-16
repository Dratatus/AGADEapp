using AGADEapp.Data.Configration;
using AGADEapp.Services;
using Microsoft.EntityFrameworkCore;

namespace AGADEapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<FileDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("FileServer")));
            builder.Services.AddDbContext<UserDBContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("UserServer")));
            //Inicjowanie bazy danych
            //add-migration InitialUser -context UserDBContext
            //add-migration InitialFile -context FileDBContext
            //update-database -context UserDBContext
            //update-database -context FileDBContext

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}