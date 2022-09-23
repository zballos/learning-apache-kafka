// See https://aka.ms/new-console-template for more information
using Core;
using Microsoft.Extensions.Hosting;
using SerializerAndDeserializer;

var builder = new HostBuilder()
    .ConfigureServices(services => services.AddMessageBusConfiguration())
    .UseConsoleLifetime()
    .Build();


Console.WriteLine("Hello, World!");

Console.WriteLine("Producing Message");

var aggregateId = Guid.NewGuid();

var messageBus = new MessageBus(KafkaConstants.MessageBusEndpoint);

var message = new EventMessage(aggregateId);

await messageBus.ProducerAsync(KafkaConstants.DemoTopicOne, message);

Console.WriteLine("Consuming Message");

await builder.StartAsync();