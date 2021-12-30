using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
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
