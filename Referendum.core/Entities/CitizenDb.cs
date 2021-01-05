using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Referendum.core.Entities
{
    [Table("CitizenDB")]
    public partial class CitizenDb
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string Opaque { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Column("SSN")]
        [StringLength(250)]
        public string Ssn { get; set; }
        [StringLength(50)]
        public string Time { get; set; }
    }
}
