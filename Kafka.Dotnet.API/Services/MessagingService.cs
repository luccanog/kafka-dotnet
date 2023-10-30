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
                AllowAutoCreateTopics = true
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();

        }

        public void Send<T>(T message)
        {
            _producer.Produce("notes", new Message<Null, string> { Value = JsonSerializer.Serialize(message) },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic");
                        }
                    });
        }
    }
}
