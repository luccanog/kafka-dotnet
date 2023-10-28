using Kafka.Dotnet.API.Storage;
using Kafka.Dotnet.API.Storage.Entities;

namespace Kafka.Dotnet.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddControllers();
            builder.Services.AddScoped<IReadonlyStorage<Note>, NoteStorage>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}