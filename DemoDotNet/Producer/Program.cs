using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

var schemaConfig = new SchemaRegistryConfig
{
    Url = "localhost:8081"
};

var schemaRegistry = new CachedSchemaRegistryClient(schemaConfig);

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

using var producer = new ProducerBuilder<string, demo.kafka.Curso>(config)
    .SetValueSerializer(new AvroSerializer<demo.kafka.Curso>(schemaRegistry))
    .Build();

var message = new Message<string, demo.kafka.Curso>
{
    Key = Guid.NewGuid().ToString(),
    Value = new demo.kafka.Curso
    {
        id = Guid.NewGuid().ToString(),
        descricao = $"Lambdas AWS Global Event: {Guid.NewGuid().ToString()}"
    } 
};

var result = await producer.ProduceAsync("courses", message);

Console.WriteLine($"{result.Offset}");