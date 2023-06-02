using System;

namespace Domain.exceptions
{
    public class ConcurentOldVersionUpdateException : Exception
    {
        public ConcurentOldVersionUpdateException(string message)
            : base(message) 
        { 
        }
    }
}
