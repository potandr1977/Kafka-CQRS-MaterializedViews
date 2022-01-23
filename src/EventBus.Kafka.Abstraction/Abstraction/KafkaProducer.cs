using Confluent.Kafka;
using Messages;
using Settings;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public class KafkaProducer<TMessage> : IDisposable, IKafkaProducer<TMessage> where TMessage : UpdateProjectionMessage
    {
        private readonly IProducer<string, TMessage> _producer;
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

            _producer = new ProducerBuilder<string, TMessage>(config).SetValueSerializer(new KafkaDeserializer<TMessage>(jsonSerializerOptions)).Build();

            _topicName = Topic;
        }


        public Task ProduceAsync(TMessage value, string key = null , int? partitionNum = null)
        {
            if (partitionNum.HasValue)
            {
                var topicPartition = new TopicPartition(_topicName, partitionNum ?? 0);
                return _producer.ProduceAsync(topicPartition, new Message<string, TMessage> { Key = key, Value = value });
            }

            return _producer.ProduceAsync(_topicName, new Message<string, TMessage> { Key = key, Value = value });
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
