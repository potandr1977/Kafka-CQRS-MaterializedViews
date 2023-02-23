using Domain.models;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public record Account : OptimisticEntity
    {
        public string Name { get; init; }

        public DateTime CreateDate { get; init; }

        public Guid PersonId { get; init; }

        public List<Guid>  Payments { get; init; }
    }
}
