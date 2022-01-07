using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public interface IKafkaProducer<TKey, TValue>
    {
        Task ProduceAsync(TKey key, TValue value, int? partitionNum = null);
    }
}
