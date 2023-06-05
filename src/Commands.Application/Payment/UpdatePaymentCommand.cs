using Domain.Enums;
using MediatR;
using System;

namespace Commands.Application.Commands
{
    public class UpdatePaymentCommand : IRequest
    {
        public Guid Id { get; init; }

        public Guid AccountId { get; init; }

        public decimal Sum { get; init; }

        public PaymentTypeEnum PaymentType { get; init; }

        public long TimeStamp { get; init; }
    }
}
