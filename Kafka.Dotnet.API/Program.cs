using Kafka.Dotnet.API.Services;
using Kafka.Dotnet.Storage;
using Kafka.Dotnet.Storage.Entities;

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
            builder.Services.AddScoped<IReadonlyStorage<Note>, ReadonlyNoteStorage>();
            builder.Services.AddScoped<IMessagingService, MessagingService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}