using Confluent.Kafka;
using EventBus.Kafka.Abstraction.Messages;
using Settings;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public class KafkaProducer<TKey, TValue> : IDisposable, IKafkaProducer<TKey, TValue> where TValue : UpdateProjectionMessage
    {
        private readonly IProducer<TKey, TValue> _producer;
        private string _topicName;

        public KafkaProducer(string Topic)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers,
            };

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _producer = new ProducerBuilder<TKey, TValue>(config).SetValueSerializer(new KafkaDeserializer<TValue>(jsonSerializerOptions)).Build();

            _topicName = Topic;
        }


        public Task ProduceAsync(TKey key, TValue value, int? partitionNum = null)
        {
            if (partitionNum.HasValue)
            {
                var topicPartition = new TopicPartition(_topicName, partitionNum ?? 0);
                return _producer.ProduceAsync(topicPartition, new Message<TKey, TValue> { Key = key, Value = value });
            }

            return _producer.ProduceAsync(_topicName, new Message<TKey, TValue> { Key = key, Value = value });
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
