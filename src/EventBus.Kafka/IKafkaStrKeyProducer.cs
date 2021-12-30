using System.Threading.Tasks;

namespace EventBus.Kafka
{
    public interface IKafkaStrKeyProducer<TValue>
    {
        Task ProduceAsync(TValue value);
    }
}
