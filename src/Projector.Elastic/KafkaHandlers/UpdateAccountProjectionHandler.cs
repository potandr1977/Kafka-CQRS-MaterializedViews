using EventBus.Kafka.Abstraction.Abstraction;
using Messages;
using Projector.Elastic.projections.Account;
using System.Threading.Tasks;

namespace Projector.Elastic.KafkaHandlers
{
    public class UpdateAccountProjectionHandler : IMessageHandler<UpdateAccountProjectionMessage>
    {
        private readonly IAccountProjector _accountProjector;

        public UpdateAccountProjectionHandler(IAccountProjector accountProjector) => _accountProjector = accountProjector;

        public Task HandleAsync(UpdateAccountProjectionMessage message) => _accountProjector.ProjectOne(message);
    }
}
