using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public interface IKafkaConsumer<TKey, TValue>
    {
        Task Consume(Action<TKey, TValue> handler, CancellationToken cancellationToken);
    }
}
