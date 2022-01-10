using DataAccess.DataAccess;
using EventBus.Kafka.Abstraction.Messages;
using Queries.Core.dataaccess;
using System;
using System.Threading.Tasks;

namespace Projector.Elastic.projections.Payment
{
    public class PaymentProjector : IPaymentProjector
    {
        private readonly IPaymentDao paymentDao;
        private readonly IPaymentSimpleViewDao paymentSimpleViewDao;

        public Task ProjectOne(UpdatePaymentProjectionMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
