using EventBus.Kafka.Abstraction;
using Messages;
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
