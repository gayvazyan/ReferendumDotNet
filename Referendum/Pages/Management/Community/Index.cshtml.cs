using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.Community
{
    public class IndexModel : PageModel
    {
        private readonly ICommunityRepasitory _communityRepasitory;
        public IndexModel(ICommunityRepasitory communityRepasitory)
        {
            _communityRepasitory = communityRepasitory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList = new List<InputModel>();
        public class InputModel : CommunitiesDb { }
        

        protected void PrepareData()
        {
            var communities = _communityRepasitory.GetAll().ToList();

            InputList = communities.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    CommunityCode = p.CommunityCode,
                    CommunityName = p.CommunityName 
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
