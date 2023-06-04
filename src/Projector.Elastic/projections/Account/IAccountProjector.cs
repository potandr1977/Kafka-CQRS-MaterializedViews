using Messages.Account;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Account
{
    public interface IAccountProjector
    {
        Task ProjectOne(UpdateAccountProjectionMessage message);

        Task ProjectOne(SaveAccountProjectionMessage message);

        Task ProjectOne(DeleteAccountProjectionMessage message);
    }
}
