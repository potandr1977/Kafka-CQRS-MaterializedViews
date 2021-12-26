using Confluent.Kafka;
using System;
using System.Text.Json;

namespace EventBus.Kafka.Abstraction
{
    public class KafkaDeserializer<T> : ISerializer<T>, IDeserializer<T>
    {
        public KafkaDeserializer(JsonSerializerOptions defaultOptions) => DefaultOptions = defaultOptions;

        JsonSerializerOptions DefaultOptions { get; }

        public byte[] Serialize(T data, SerializationContext context)
            => JsonSerializer.SerializeToUtf8Bytes(data, GetMessageType(), DefaultOptions);

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
            => (T)JsonSerializer.Deserialize(data, GetMessageType(), DefaultOptions);

        static Type GetMessageType()
            => typeof(T);
    }
}
