using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Referendum.core.Entities
{

    [Table("CommunitiesDB")]
    public partial class CommunitiesDb
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string CommunityCode { get; set; }
        [StringLength(50)]
        public string CommunityName { get; set; }
    }
}