using Confluent.Kafka;
using System.Text.Json;

namespace Kafka.Dotnet.API.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IProducer<Null, string> _producer;

        public MessagingService(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();

        }

        public async Task SendAsync<T>(T message)
        {
            var result = await _producer.ProduceAsync("notes", new Message<Null, string> { Value = JsonSerializer.Serialize(message) });
            Console.WriteLine(result.Value);
        }
    }
}
