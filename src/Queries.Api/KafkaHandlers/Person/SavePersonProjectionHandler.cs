using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Persons;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers.Person
{
    public class SavePersonProjectionHandler : IMessageHandler<SavePersonProjectionMessage>
    {
        public Task HandleAsync(SavePersonProjectionMessage message)
        {
            Console.WriteLine("Save Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }

}
