using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Referendum.Model
{
    [Table("Table_1")]
    public partial class Table1
    {
        public int Id { get; set; }
        [Column("hhh")]
        [StringLength(10)]
        public string Hhh { get; set; }
        [Column("hhhgg")]
        [StringLength(10)]
        public string Hhhgg { get; set; }
    }
}
