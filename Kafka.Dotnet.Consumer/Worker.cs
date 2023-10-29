using System.Collections.Generic;
using Confluent.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Dotnet.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer<Ignore,string> _consumer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            var config = new ConsumerConfig
            {
                BootstrapServers = "host1:9092,host2:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
           
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}