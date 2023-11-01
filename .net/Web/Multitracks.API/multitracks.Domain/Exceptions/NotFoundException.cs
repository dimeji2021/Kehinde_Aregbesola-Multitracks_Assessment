using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace multitracks.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}
