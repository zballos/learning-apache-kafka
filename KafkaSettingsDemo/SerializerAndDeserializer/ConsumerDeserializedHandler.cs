using Core;
using Microsoft.Extensions.Hosting;

namespace SerializerAndDeserializer
{
    public class ConsumerDeserializedHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerDeserializedHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await SetSubscribersAsync(stoppingToken);
        }

        private async Task SetSubscribersAsync(CancellationToken stoppingToken)
        {
            await _bus.ConsumerAsync<EventMessage>(KafkaConstants.DemoTopicOne, CancelOperation, stoppingToken);
        }

        private async Task CancelOperation(EventMessage message)
        {
            Console.WriteLine($"{message.MessageType} > {message.AggregateId}");
        }
    }
}
