using System;

namespace Messages
{
    public record UpdateAccountProjectionMessage : UpdateProjectionMessage
    {
        public Guid AccountId { get; set; }

        public string Name { get; set; }

        public Guid PersonId { get; set; }
    }
}
