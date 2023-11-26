using EventBus.Kafka.Abstraction.Abstraction;
using Messages.Persons;
using Projector.Elastic.projections.Person;
using System.Threading.Tasks;

namespace SimpleViewProjector.Elastic.KafkaHandlers.Person
{
    public class SavePersonProjectionHandler : IMessageHandler<SavePersonProjectionMessage>
    {
        private readonly IPersonProjector _personProjector;

        public SavePersonProjectionHandler(IPersonProjector personProjector) => _personProjector = personProjector;

        public Task HandleAsync(SavePersonProjectionMessage message) => _personProjector.ProjectOne(message);
    }

}
