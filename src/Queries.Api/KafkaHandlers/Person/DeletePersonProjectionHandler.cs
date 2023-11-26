using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Persons;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers.Person
{
    public class DeletePersonProjectionHandler : IMessageHandler<DeletePersonProjectionMessage>
    {
        public Task HandleAsync(DeletePersonProjectionMessage message)
        {
            Console.WriteLine("Delete Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }

}
