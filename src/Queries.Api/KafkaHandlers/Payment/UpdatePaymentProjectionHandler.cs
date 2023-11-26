using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Payments;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers.Payment
{
    public class UpdatePaymentProjectionHandler : IMessageHandler<UpdatePaymentProjectionMessage>
    {
        public Task HandleAsync(UpdatePaymentProjectionMessage message)
        {
            Console.WriteLine("update Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }
}
