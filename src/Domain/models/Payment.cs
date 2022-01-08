using Domain.Enums;
using System;

namespace Domain.Models
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public decimal Sum { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
    }
}
