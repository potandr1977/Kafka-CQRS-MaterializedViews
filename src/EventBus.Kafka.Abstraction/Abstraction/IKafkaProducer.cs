using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public interface IKafkaProducer<TMessage>
    {
        Task ProduceAsync( TMessage value, string key = null, int? partitionNum = null);
    }
}
