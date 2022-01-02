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
        private bool switcher;
        public KafkaStrKeyProducer(string TopicName) : base(TopicName)
        {
            switcher = true;
        }

        public Task ProduceAsync(TValue value)
        {
            switcher = !switcher;
            var key = $"{GetType().Name}{(switcher?1:0)}";

            return ProduceAsync(key,value);
        }

    }
}
