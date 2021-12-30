using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaAccountProducer: KafkaStrKeyProducer<UpdateAccountProjectionMessage>, IKafkaAccountProducer
    {
        public KafkaAccountProducer() : base(KafkaSettings.AccountTopicName)
        {
        }
    }
}
