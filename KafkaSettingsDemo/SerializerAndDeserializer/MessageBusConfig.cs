using Microsoft.Extensions.DependencyInjection;

namespace SerializerAndDeserializer
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services)
        {
            services.AddMessageBus(KafkaConstants.MessageBusEndpoint)
                .AddHostedService<ConsumerDeserializedHandler>();
        }
    }
}
