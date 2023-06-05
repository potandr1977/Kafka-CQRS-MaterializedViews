using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Account;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers
{
    public class SaveAccountProjectionHandler : IMessageHandler<SaveAccountProjectionMessage>
    {
        public Task HandleAsync(SaveAccountProjectionMessage message)
        {
            Console.WriteLine("Save Account projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }
}
