using Kafka.Dotnet.Storage.Entities;
using Kafka.Dotnet.Storage;

namespace Kafka.Dotnet.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                    services.AddScoped<IStorage<Note>, NoteStorage>();
                    services.AddDbContext<AppDbContext>();
                })
                .Build();

            host.Run();
        }
    }
}