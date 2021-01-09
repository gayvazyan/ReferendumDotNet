using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Referendum.core.Entities
{

    public  class CommunitiesDb
    {
        public int Id { get; set; }
        public string CommunityCode { get; set; }
        public string CommunityName { get; set; }
    }
}