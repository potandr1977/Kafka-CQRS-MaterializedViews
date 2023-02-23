using Domain.Enums;
using Domain.models;
using System;

namespace Domain.Models
{
    public record Payment : OptimisticEntity
    {
        public Guid AccountId { get; init; }

        public decimal Sum { get; init; }

        public DateTime CreateDate { get; init; }

        public PaymentTypeEnum PaymentType { get; init; }
    }
}
