using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ConfluentExtensions
    {
        public static void ConfigureKafkaProducer(this IServiceCollection services)
        {
            var settings = services.GetOptions<EventBusSettings>(nameof(EventBusSettings));
            if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
                throw new ArgumentNullException("EventBusSettings is not configured properly!");
            var kafkaConnection = settings.HostAddress;  // Kafka broker address (e.g., "localhost:9092")
            services.AddSingleton<IProducer<Null, string>>(sp =>
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = kafkaConnection
                };
                var producer = new ProducerBuilder<Null, string>(config).Build();
                return producer;
            });
        }
    }
}
