using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Referendum.core.Entities
{
    public  class CitizenDb
    {
        public int Id { get; set; }
        public string Opaque { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public string Time { get; set; }
        public int? ReferendumId { get; set; }
        
    }
}
