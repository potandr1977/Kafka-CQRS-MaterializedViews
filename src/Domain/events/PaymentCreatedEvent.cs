using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.events
{
    public record PaymentCreatedEvent
    {
        public Guid Id { get; init; }

        public Guid AccountId { get; init; }

        public decimal Sum { get; init; }
    }
}
