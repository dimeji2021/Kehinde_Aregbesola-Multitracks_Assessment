using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multitracks.Domain.Exceptions
{
    public class Error
    {
        public string ErrorSource { get; set; }
        public object Type { get; set; }
    }
}
