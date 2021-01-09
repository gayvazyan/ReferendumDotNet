using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.ReferendumT
{
    public class IndexModel : PageModel
    {
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;
        public IndexModel(IReferendumRepasitory referendumRepasitory, 
                          ICommunityRepasitory communityRepasitory)

        {
            _referendumRepasitory = referendumRepasitory;
            _communityRepasitory = communityRepasitory;
            InputList = new List<InputModel>();

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList = new List<InputModel>();
        public class InputModel : ReferendumDb
        {
            public string CommunityName { get; set; }
        }

        protected void PrepareData()
        {
            var referendum = _referendumRepasitory.GetAll().ToList();

            InputList = referendum.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    Question = p.Question,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    ConnectionCount = p.ConnectionCount,
                    IsActive = p.IsActive,
                    CommunityId = p.CommunityId,
                    CommunityName = (p.CommunityId!=null) ?  _communityRepasitory.GetAll().FirstOrDefault(f=>f.Id==p.CommunityId).CommunityName : "Համապետական հանրաքվե"
                };
            }).ToList();
        }
        public void OnGet()
        {
            PrepareData();
        }

        public ActionResult OnPost()
        {
            PrepareData();
            return Page();
        }
    }
}
