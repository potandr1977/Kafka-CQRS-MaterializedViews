using Messages;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Account
{
    public interface IAccountProjector
    {
        Task ProjectOne(UpdateAccountProjectionMessage message);
    }
}
