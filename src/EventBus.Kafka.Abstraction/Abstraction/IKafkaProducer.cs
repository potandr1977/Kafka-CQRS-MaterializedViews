using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public interface IKafkaProducer<TKey, TValue>
    {
        Task ProduceAsync(TKey key, TValue value);
    }
}
