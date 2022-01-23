using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public interface IKafkaConsumer<TMessage>
    {
        Task Consume(CancellationToken cancellationToken);

        Task Consume(int? partition, int? offset, CancellationToken stoppingToken);
    }
}
