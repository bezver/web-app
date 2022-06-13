using System;
using System.Net;

namespace Core.Exceptions
{
    class ServiceException: Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ServiceException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
