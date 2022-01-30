using EventBus.Kafka.Abstraction.Abstraction;
using Messages;
using Projector.Elastic.projections.Person;
using System.Threading.Tasks;

namespace Projector.Elastic.KafkaHandlers
{
    public class UpdatePersonProjectionHandler : IMessageHandler<UpdatePersonProjectionMessage>
    {
        private readonly IPersonProjector _personProjector;

        public UpdatePersonProjectionHandler(IPersonProjector personProjector) => _personProjector = personProjector;

        public Task HandleAsync(UpdatePersonProjectionMessage message) => _personProjector.ProjectOne(message);
    }

}
