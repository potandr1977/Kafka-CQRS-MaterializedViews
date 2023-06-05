using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Account;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers
{
    public class DeleteAccountProjectionHandler : IMessageHandler<DeleteAccountProjectionMessage>
    {
        public Task HandleAsync(DeleteAccountProjectionMessage message)
        {
            Console.WriteLine("Delete Account projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }
}
