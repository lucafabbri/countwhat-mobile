using System;
using System.Net;

namespace countwhat.Services
{
    internal class HttpClientErrorException : Exception
    {
        private HttpStatusCode statusCode;

        public HttpClientErrorException()
        {
        }

        public HttpClientErrorException(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public HttpClientErrorException(string message) : base(message)
        {
        }

        public HttpClientErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}