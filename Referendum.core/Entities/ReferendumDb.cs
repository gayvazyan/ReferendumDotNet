using System;
using System.ComponentModel.DataAnnotations;

namespace Referendum.core.Entities
{
    public  class ReferendumDb
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Լրացրեք հանրաքվեի հարցը")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Ընտրեք ստորագրահավաքի սկիզբի ամսաթիվը")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "Ընտրեք ստորագրահավաքի ավարտի ամսաթիվը")]
        public DateTime? EndDate { get; set; }
        public int? ConnectionCount { get; set; }
        public bool? IsActive { get; set; }
        public int? CommunityId { get; set; }
        
    }
}
