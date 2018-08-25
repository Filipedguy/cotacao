using System;
using System.Collections.Generic;
using System.Text;

namespace LastCurrencyAPI.Core.Domain.Exceptions
{
    public class ServiceRequestFailedException : Exception
    {
        public ServiceRequestFailedException(string serviceName, string message, Exception innerException) : base(message, innerException)
        {
            ServiceName = serviceName;
        }

        public string ServiceName { get; set; }
    }
}
