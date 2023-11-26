using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Payments;
using Projector.Elastic.projections.Payment;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Payment
{
    public class UpdatePaymentProjectionHandler : IMessageHandler<UpdatePaymentProjectionMessage>
    {
        private readonly IPaymentProjector _paymentProjector;

        public UpdatePaymentProjectionHandler(IPaymentProjector personProjector) => _paymentProjector = personProjector;

        public Task HandleAsync(UpdatePaymentProjectionMessage message) => _paymentProjector.ProjectOne(message);
    }
}
