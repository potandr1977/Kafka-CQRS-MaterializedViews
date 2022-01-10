using Queries.Core.Enums;
using System;

namespace Queries.Core.models
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string AccountName { get; set; }

        public decimal Sum { get; set; }

        public PaymentTypeEnum PaymentType { get; set; }
    }
}
