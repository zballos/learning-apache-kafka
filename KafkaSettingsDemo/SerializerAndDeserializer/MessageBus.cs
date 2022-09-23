using Confluent.Kafka;
using Core;

namespace SerializerAndDeserializer
{
    internal class MessageBus : IMessageBus
    {
        private readonly string _bootstrapServer;

        public MessageBus(string bootstrapServer)
        {
            _bootstrapServer = bootstrapServer;
        }

        public Task ConsumerAsync<T>(string topic, Func<T, Task> onMessage, CancellationToken cancellationToken) where T : EventMessage
        {
            _ = Task.Factory.StartNew(async () =>
            {
                var config = new ConsumerConfig
                {
                    GroupId = "group-1",
                    BootstrapServers = _bootstrapServer,
                    EnableAutoCommit = false,
                    EnablePartitionEof = true,
                };

                using var consumer = new ConsumerBuilder<string, T>(config)
                    .SetValueDeserializer(new KafkaDeserializer<T>())
                    .Build();

                consumer.Subscribe(topic);

                while(!cancellationToken.IsCancellationRequested)
                {
                    var result = consumer.Consume();

                    if (result.IsPartitionEOF)
                    {
                        continue;
                    }

                    await onMessage(result.Message.Value);

                    consumer.Commit();
                }
            }, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            
            return Task.CompletedTask;
        }

        public async Task ProducerAsync<T>(string topic, T message) where T : EventMessage
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServer
            };

            var producer = new ProducerBuilder<string, T>(config)
                .SetValueSerializer(new KafkaSerializer<T>())
                .Build();

            var result = await producer.ProduceAsync(topic, new Message<string, T>
            {
                Key = Guid.NewGuid().ToString(),
                Value = message
            });

            await Task.CompletedTask;
        }
    }

    public interface IMessageBus
    {
        Task ProducerAsync<T>(string topic, T message) where T : EventMessage;
        Task ConsumerAsync<T>(string topic, Func<T, Task> onMessage, CancellationToken cancellationToken) where T : EventMessage;
    }
}
