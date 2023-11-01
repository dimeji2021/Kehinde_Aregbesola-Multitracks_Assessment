using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace multitracks.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BaseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public BaseException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
