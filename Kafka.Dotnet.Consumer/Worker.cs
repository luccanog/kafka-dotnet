using Confluent.Kafka;
using Kafka.Dotnet.Storage;
using Kafka.Dotnet.Storage.Entities;
using System.Text.Json;

namespace Kafka.Dotnet.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IStorage<Note> _readonlyStorage;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IStorage<Note> readonlyStorage)
        {
            _logger = logger;

            var config = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            _readonlyStorage = readonlyStorage;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("notes");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var consumeResult = _consumer.Consume(stoppingToken);
                var note = JsonSerializer.Deserialize<Note>(consumeResult.Message.Value);
                _readonlyStorage.Add(note);
            }
            _consumer.Close();
        }

        ~Worker()
        {
            _consumer.Dispose();
        }
    }
}