using Confluent.Kafka;
using EventBus.Kafka.Abstraction.Messages;
using Settings;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public class KafkaStrKeyProducer<TValue>: KafkaProducer<string, TValue>, IKafkaStrKeyProducer<TValue> 
        where TValue : UpdateProjectionMessage
    {
        public KafkaStrKeyProducer(string TopicName) : base(TopicName)
        {

        }

        public Task ProduceAsync(TValue value)
        {
            var key = $"{GetType().Name}-{value.Id}";

            return ProduceAsync(key,value);
        }

    }
}
