using MediatR;
using Queries.Core.models;
using System.Collections.Generic;

namespace Queries.Application.Payments
{
    public class GetAllPaymentQuery : IRequest<IReadOnlyCollection<Payment>>
    {
    }
}
