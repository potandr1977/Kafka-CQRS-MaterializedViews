using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Account;
using Projector.Elastic.projections.Account;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Account
{
    public class UpdateAccountProjectionHandler : IMessageHandler<UpdateAccountProjectionMessage>
    {
        private readonly IAccountProjector _accountProjector;

        public UpdateAccountProjectionHandler(IAccountProjector accountProjector) => _accountProjector = accountProjector;

        public Task HandleAsync(UpdateAccountProjectionMessage message) => _accountProjector.ProjectOne(message);
    }
}
