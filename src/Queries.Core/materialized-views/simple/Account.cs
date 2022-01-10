using System;

namespace Queries.Core.models
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid PersonId { get; set; }

        public string PersonName { get; set; }
    }
}
