using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Account;
using Projector.Elastic.projections.Account;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Account
{
    public class DeleteAccountProjectionHandler : IMessageHandler<DeleteAccountProjectionMessage>
    {
        private readonly IAccountProjector _accountProjector;

        public DeleteAccountProjectionHandler(IAccountProjector accountProjector) => _accountProjector = accountProjector;

        public Task HandleAsync(DeleteAccountProjectionMessage message) => _accountProjector.ProjectOne(message);
    }
}
