using Messages.Payments;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Payment
{
    public interface IPaymentProjector
    {
        Task ProjectOne(UpdatePaymentProjectionMessage message);

        Task ProjectOne(SavePaymentProjectionMessage message);

        Task ProjectOne(DeletePaymentProjectionMessage message);
    }
}
