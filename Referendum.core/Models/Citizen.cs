using System;
using System.Collections.Generic;
using System.Text;

namespace Referendum.core
{
   public class Citizen
    {
        public int Id { get; set; }
        public string Opaque { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string Time { get; set; }
    }
}
