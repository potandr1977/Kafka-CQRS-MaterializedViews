using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Account;
using Projector.Elastic.projections.Account;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Account
{
    public class SaveAccountProjectionHandler : IMessageHandler<SaveAccountProjectionMessage>
    {
        private readonly IAccountProjector _accountProjector;

        public SaveAccountProjectionHandler(IAccountProjector accountProjector) => _accountProjector = accountProjector;

        public Task HandleAsync(SaveAccountProjectionMessage message) => _accountProjector.ProjectOne(message);
    }
}
