using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Payment;
using Projector.Elastic.projections.Payment;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Payment
{
    public class SavePaymentProjectionHandler : IMessageHandler<SavePaymentProjectionMessage>
    {
        private readonly IPaymentProjector _paymentProjector;

        public SavePaymentProjectionHandler(IPaymentProjector personProjector) => _paymentProjector = personProjector;

        public Task HandleAsync(SavePaymentProjectionMessage message) => _paymentProjector.ProjectOne(message);
    }
}
