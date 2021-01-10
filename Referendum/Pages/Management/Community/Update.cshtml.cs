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
    public class UpdateModel : PageModel
    {
        private readonly ICommunityRepasitory _communityRepasitory;
        public UpdateModel(ICommunityRepasitory communityRepasitory)
        {
            _communityRepasitory = communityRepasitory;
            Update = new UpdateCommunityModel();
        }
        public class UpdateCommunityModel : CommunitiesDb { }

        [BindProperty]
        public UpdateCommunityModel Update { get; set; }

        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public void OnGet(int id)
        {
            var result = _communityRepasitory.GetByID(id);

            if (result != null)
            {
                Update.Id = result.Id;
                Update.CommunityCode = result.CommunityCode;
                Update.CommunityName = result.CommunityName;
            }
        }

        public ActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var community = _communityRepasitory.GetByID(Update.Id);

                    community.CommunityCode = Update.CommunityCode;
                    community.CommunityName = Update.CommunityName;


                    _communityRepasitory.Update(community);

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
