using Domain.dataaccess;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public interface IPaymentDao : IDao<Payment>
    {
        public Task<List<Payment>> GetByAccountId(Guid accountId);
    }
}
