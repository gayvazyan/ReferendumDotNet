using System;
using System.Collections.Generic;
using System.Text;

namespace Referendum.core.Entities
{
   public class UserDb
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
