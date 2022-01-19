using System;
using System.Collections.Generic;
using System.Text;

namespace Messages
{
    public record UpdatePersonProjectionMessage : UpdateProjectionMessage
    {
        public Guid PersonId { get; set; }

        public string Name {get; set; }

        public string Inn { get; set; }
    }
}
