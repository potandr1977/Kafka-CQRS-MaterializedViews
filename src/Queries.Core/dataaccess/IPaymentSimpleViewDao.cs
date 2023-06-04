using Queries.Core.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Queries.Core.dataaccess
{
    public interface IPaymentSimpleViewDao
    {
        Task<IReadOnlyCollection<Payment>> GetAll();

        Task Save(Payment payment);

        Task Update(Payment payment);

        Task Delete(string Id);
    }
}
