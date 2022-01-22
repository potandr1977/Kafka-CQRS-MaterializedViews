using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public interface IMessageHandler<TValue>
    {
        Task HandleAsync(TValue value);
    }
}
