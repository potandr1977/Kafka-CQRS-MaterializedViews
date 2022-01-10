using EventBus.Kafka.Abstraction.Messages;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public class KafkaStrKeyProducer<TValue>: KafkaProducer<string, TValue>, IKafkaStrKeyProducer<TValue> 
        where TValue : UpdateProjectionMessage
    {
        public KafkaStrKeyProducer(string TopicName) : base(TopicName)
        {
        }

        public Task ProduceAsync(TValue value, int? partitionNum = null)
        {
            var key = $"{GetType().Name}";

            return ProduceAsync(key, value, partitionNum);
        }

    }
}
