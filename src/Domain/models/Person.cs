using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public record Person
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Inn { get; init; }

        public DateTime CreateDate { get; init; }

        public List<Guid> AccountIds { get; init; }


    }
}
