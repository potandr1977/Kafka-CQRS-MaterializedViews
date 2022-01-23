using EventBus.Kafka.Abstraction.Abstraction;
using Messages;
using System;
using System.Threading.Tasks;

namespace Queries.Api.KafkaHandlers
{
    public class UpdatePaymentProjectionHandler: IMessageHandler<UpdatePaymentProjectionMessage>
    {
        public Task HandleAsync(UpdatePaymentProjectionMessage message)
        {
            Console.WriteLine("Payment projection changed AccountId:{value.Id}");

            return Task.CompletedTask;
        }
    }
}
