using EventBus.Kafka.Abstraction.Messages;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Person
{
    public interface IPersonProjector
    {
        Task ProjectOne(UpdatePersonProjectionMessage message);
    }
}
