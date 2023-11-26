using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Persons;
using Projector.Elastic.projections.Person;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Person
{
    public class DeletePersonProjectionHandler : IMessageHandler<DeletePersonProjectionMessage>
    {
        private readonly IPersonProjector _personProjector;

        public DeletePersonProjectionHandler(IPersonProjector personProjector) => _personProjector = personProjector;

        public Task HandleAsync(DeletePersonProjectionMessage message) => _personProjector.ProjectOne(message);
    }

}
