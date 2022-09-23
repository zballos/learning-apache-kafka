// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Core;

Console.WriteLine("Hello, World!");

const string topic = "whatever";

var config = new ProducerConfig
{
    Acks = Acks.All,
    BootstrapServers = KafkaConstants.MessageBusEndpoint
};

var message = $"Test > {Guid.NewGuid()}";

try
{
    using var producer = new ProducerBuilder<Null, string>(config).Build();

    var result = await producer.ProduceAsync(topic, new Message<Null, string>
    {
        Value = message
    });

    Console.WriteLine($"Success > {result.Value}");
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.ToString());
}
