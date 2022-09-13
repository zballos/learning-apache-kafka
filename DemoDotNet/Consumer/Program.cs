using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

var schemaConfig = new SchemaRegistryConfig
{
    Url = "localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ConsumerConfig 
{ 
    GroupId = "test",
    BootstrapServers = "localhost:9092"
};

using var consumer = new ConsumerBuilder<string, demo.kafka.Curso>(config)
    .SetValueDeserializer(new AvroDeserializer<demo.kafka.Curso>(schemaRegistry).AsSyncOverAsync())
    .Build();

consumer.Subscribe("courses");

while (true)
{
    var result = consumer.Consume();
    Console.WriteLine($"Message {result.Message.Value.descricao}");
}