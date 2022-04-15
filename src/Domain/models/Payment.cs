using Domain.Enums;
using System;

namespace Domain.Models
{
    public record Payment
    {
        public Guid Id { get; init; }

        public Guid AccountId { get; init; }

        public decimal Sum { get; init; }

        public DateTime CreateDate { get; init; }

        public PaymentTypeEnum PaymentType { get; init; }
    }
}
