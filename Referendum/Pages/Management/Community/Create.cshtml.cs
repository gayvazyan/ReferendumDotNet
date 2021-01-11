using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.Community
{
    public class CreateModel : PageModel
    {
        private readonly ICommunityRepasitory _communityRepasitory;
        public CreateModel(ICommunityRepasitory communityRepasitory)
        {
            _communityRepasitory = communityRepasitory;
            Create = new CreateCommunityModel();
        }

        public class CreateCommunityModel : CommunitiesDb { }

        [BindProperty]
        public CreateCommunityModel Create { get; set; }


        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }
        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var community = new CommunitiesDb
                    {
                        Id = Create.Id,
                        CommunityCode = Create.CommunityCode,
                        CommunityName = Create.CommunityName
                    };

                    var result = _communityRepasitory.Insert(community);

                    if (result != null)
                        return RedirectToPage("/Management/Community/Index");

                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "005", Description = ex.Message });
                }
            }
            return Page();
        }
    }
}
