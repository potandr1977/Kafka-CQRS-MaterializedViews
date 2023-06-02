using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.models
{
    public record Person
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Inn { get; init; }

        public DateTime TimeStamp { get; init; }
    }
}
