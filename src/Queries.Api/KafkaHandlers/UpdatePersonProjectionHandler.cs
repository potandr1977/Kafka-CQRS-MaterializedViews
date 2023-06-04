using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Person;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers
{
    public class UpdatePersonProjectionHandler : IMessageHandler<UpdatePersonProjectionMessage>
    {
        public Task HandleAsync(UpdatePersonProjectionMessage message)
        {
            Console.WriteLine("Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }

}
