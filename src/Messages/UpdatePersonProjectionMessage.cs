using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public record UpdatePersonProjectionMessage : UpdateProjectionMessage
    {
        public Guid PersonId { get; init; }

        public string Name {get; init; }

        public string Inn { get; init; }
    }
}
