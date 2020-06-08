using System;

namespace ASB.Common.Infrastructure
{
    public class MicroserviceException: Exception
    {
        public MicroserviceException(string message) : base(message){}
    }
}