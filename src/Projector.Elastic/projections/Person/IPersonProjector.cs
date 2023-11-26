using Messages.Persons;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Person
{
    public interface IPersonProjector
    {
        Task ProjectOne(UpdatePersonProjectionMessage message);

        Task ProjectOne(SavePersonProjectionMessage message);

        Task ProjectOne(DeletePersonProjectionMessage message);
    }
}
