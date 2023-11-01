using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace multitracks.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base(HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}
