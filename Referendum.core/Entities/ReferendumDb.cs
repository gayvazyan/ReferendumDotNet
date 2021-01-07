using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Referendum.core.Entities
{
    [Table("ReferendumDB")]
    public partial class ReferendumDb
    {
        public int Id { get; set; }
        [StringLength(500)]
        public string Question { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public int? ConnectionCount { get; set; }
        
    }
}
