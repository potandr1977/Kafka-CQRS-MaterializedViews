using EventBus.Kafka.Abstraction.Abstraction;
using Messages;
using Projector.Elastic.projections.Payment;
using Projector.Elastic.projections.Person;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.KafkaHandlers
{
    public class UpdatePaymentProjectionHandler: IMessageHandler<UpdatePaymentProjectionMessage>
    {
        private readonly IPaymentProjector _paymentProjector;

        public UpdatePaymentProjectionHandler(IPaymentProjector personProjector) => _paymentProjector = personProjector;

        public Task HandleAsync(UpdatePaymentProjectionMessage message) => _paymentProjector.ProjectOne(message);
    }
}
