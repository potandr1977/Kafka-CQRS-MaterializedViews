﻿using System;

namespace Queries.Core.models
{
    public record Account
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string PersonName { get; init; }

        public long TimeStamp { get; init; }
    }
}
