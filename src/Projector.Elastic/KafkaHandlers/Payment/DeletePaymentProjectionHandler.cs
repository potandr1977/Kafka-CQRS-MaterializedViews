using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Payments;
using Projector.Elastic.projections.Payment;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Payment
{
    public class DeletePaymentProjectionHandler : IMessageHandler<DeletePaymentProjectionMessage>
    {
        private readonly IPaymentProjector _paymentProjector;

        public DeletePaymentProjectionHandler(IPaymentProjector personProjector) => _paymentProjector = personProjector;

        public Task HandleAsync(DeletePaymentProjectionMessage message) => _paymentProjector.ProjectOne(message);
    }
}
