using EventBus.Kafka.Abstraction;
using Messages;
using Settings;

namespace EventBus.Kafka
{
    public class KafkaPersonConsumer : KafkaConsumer<string, UpdatePersonProjectionMessage>, IKafkaPersonConsumer
    {
        public KafkaPersonConsumer() : base(KafkaSettings.BusinessGroupId, KafkaSettings.PersonTopicName)
        {
        }
    }
}
