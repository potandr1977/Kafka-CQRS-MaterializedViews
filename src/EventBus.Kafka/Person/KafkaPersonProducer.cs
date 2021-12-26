using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaPersonProducer: KafkaProducer<string, UpdatePersonProjectionMessage>, IKafkaPersonProducer
    {
        public KafkaPersonProducer():base(KafkaSettings.AccountTopicName)
        {
        }
    }
}
