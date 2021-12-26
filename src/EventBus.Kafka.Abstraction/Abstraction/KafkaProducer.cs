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
        /// <summary>  
        /// Initializes the producer  
        /// </summary>  
        /// <param name="config"></param>  
        public KafkaProducer(string Topic)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = $"{KafkaSettings.Host}:{KafkaSettings.Port}"
            };

            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _producer = new ProducerBuilder<TKey, TValue>(config).SetValueSerializer(new KafkaDeserializer<TValue>(jsonSerializerOptions)).Build();

            _topicName = Topic;
        }

        /// <summary>  
        /// Triggered when the service creates Kafka topic.  
        /// </summary>  
        /// <param name="topic">Indicates topic name</param>  
        /// <param name="key">Indicates message's key in Kafka topic</param>  
        /// <param name="value">Indicates message's value in Kafka topic</param>  
        /// <returns></returns>  
        public async Task ProduceAsync(TValue value)
        {
            var key = default(TKey);
            await _producer.ProduceAsync(_topicName, new Message<TKey, TValue> { Key = key, Value = value });
        }

        /// <summary>  
        ///   
        /// </summary>  
        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
