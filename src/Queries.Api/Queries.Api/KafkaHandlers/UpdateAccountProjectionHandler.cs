using EventBus.Kafka.Abstraction.Abstraction;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers
{
    public class UpdateAccountProjectionHandler : IMessageHandler<UpdateAccountProjectionMessage>
    {
        public Task HandleAsync(UpdateAccountProjectionMessage message)
        {
            Console.WriteLine("Account projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }
}
