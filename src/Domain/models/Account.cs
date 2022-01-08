using System;

namespace Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid PersonId { get; set; }

    }
}
