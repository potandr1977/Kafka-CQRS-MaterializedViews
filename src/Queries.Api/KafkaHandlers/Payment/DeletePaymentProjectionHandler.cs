using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Payment;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers.Payment
{
    public class DeletePaymentProjectionHandler : IMessageHandler<DeletePaymentProjectionMessage>
    {
        public Task HandleAsync(DeletePaymentProjectionMessage message)
        {
            Console.WriteLine("Delete Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }
}
