using Messages;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Payment
{
    public interface IPaymentProjector
    {
        Task ProjectOne(UpdatePaymentProjectionMessage message);
    }
}
