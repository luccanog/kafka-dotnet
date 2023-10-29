using Confluent.Kafka;

namespace Kafka.Dotnet.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer<Ignore, string> _consumer;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;

            var config = new ConsumerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                GroupId = configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("topic");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var consumeResult = _consumer.Consume(stoppingToken);
            }
            _consumer.Close();
        }

        ~Worker()
        {
            _consumer.Dispose();
        }
    }
}