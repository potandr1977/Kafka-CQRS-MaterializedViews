using EventBus.Kafka.Abstraction;
using Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaPersonProducer: KafkaStrKeyProducer<UpdatePersonProjectionMessage>, IKafkaPersonProducer
    {
        public KafkaPersonProducer() : base(KafkaSettings.PersonTopicName)
        {
        }
    }
}
