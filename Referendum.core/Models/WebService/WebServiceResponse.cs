using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core.Models
{
    public class WebServiceResponse
    {
        public string Status { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
    }
}
