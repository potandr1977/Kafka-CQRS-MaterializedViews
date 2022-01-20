using Queries.Core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.dataaccess
{
    public interface IPaymentSimpleViewDao
    {
        Task<IReadOnlyCollection<Payment>> GetAll();

        Task Save(Payment payment);
    }
}
