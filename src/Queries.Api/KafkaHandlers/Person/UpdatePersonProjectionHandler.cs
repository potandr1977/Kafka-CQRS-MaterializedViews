using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Persons;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers.Person
{
    public class UpdatePersonProjectionHandler : IMessageHandler<UpdatePersonProjectionMessage>
    {
        public Task HandleAsync(UpdatePersonProjectionMessage message)
        {
            Console.WriteLine("Update Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }

}
