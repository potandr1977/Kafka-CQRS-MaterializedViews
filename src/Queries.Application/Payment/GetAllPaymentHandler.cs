using MediatR;
using Queries.Core.dataaccess;
using Queries.Core.models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Application.Payments
{
    public class GetAllPaymentHandler : IRequestHandler<GetAllPaymentQuery, IReadOnlyCollection<Payment>>
    {
        private readonly IPaymentSimpleViewDao _paymentSimpleViewDao;

        public GetAllPaymentHandler(IPaymentSimpleViewDao paymentSimpleViewDao) =>
            _paymentSimpleViewDao = paymentSimpleViewDao;

        public Task<IReadOnlyCollection<Payment>> Handle(GetAllPaymentQuery request, CancellationToken cancellationToken) =>
            _paymentSimpleViewDao.GetAll();
    }
}
