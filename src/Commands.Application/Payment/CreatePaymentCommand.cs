using Domain.Enums;
using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class CreatePaymentCommand : IRequest
    {
        public Guid AccountId { get; init; }

        public decimal Sum { get; init; }

        public PaymentTypeEnum PaymentType { get; init; }
    }
}
