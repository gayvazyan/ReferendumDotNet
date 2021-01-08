using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core.Models
{
    public class PassportWebServiceResponse
    {
        public string Status { get; set; }
        public string Opaque { get; set; }
        public string PNum { get; set; }
        public string Full_name { get; set; }
        public string AVVRegistrationAddress { get; set; }
        public string ErrorMessage { get; set; }
    }
}
