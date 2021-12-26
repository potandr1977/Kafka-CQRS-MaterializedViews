using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaAccountProducer: KafkaProducer<string, UpdateAccountProjectionMessage>, IKafkaAccountProducer
    {
        public KafkaAccountProducer():base(KafkaSettings.AccountTopicName)
        {
        }
    }
}
