using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Payments;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers.Payment
{
    public class SavePaymentProjectionHandler : IMessageHandler<SavePaymentProjectionMessage>
    {
        public Task HandleAsync(SavePaymentProjectionMessage message)
        {
            Console.WriteLine("save Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }
}
