using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SerializerAndDeserializer
{
    internal class KafkaDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using var stream = new MemoryStream();
            using var zipStream = new GZipStream(stream, CompressionMode.Decompress, true);
            
            return JsonSerializer.Deserialize<T>(zipStream);
        }
    }
}
