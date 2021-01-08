using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core.Models
{
    public class PassportDataResponse
    {
        public string Opaque { get; set; }
        public PassportDataContent Data { get; set; }
    }

    public class PassportDataContent
    {
        public string PNum { get; set; }
        public string Full_name { get; set; }
        public string AVVRegistrationAddress { get; set; }
    }
}
