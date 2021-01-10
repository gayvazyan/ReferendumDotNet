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
    public class DeleteModel : PageModel
    {
        private readonly ICommunityRepasitory _communityRepasitory;
        public DeleteModel(ICommunityRepasitory communityRepasitory)
        {
            _communityRepasitory = communityRepasitory;
            Delete = new DeleteCommunityModel();
        }

        public class DeleteCommunityModel : CommunitiesDb { }

        [BindProperty]
        public DeleteCommunityModel Delete { get; set; }



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
                Delete.Id = result.Id;
                Delete.CommunityCode = result.CommunityCode;
                Delete.CommunityName = result.CommunityName;
            }
        }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var community = _communityRepasitory.GetByID(Delete.Id);

                    _communityRepasitory.Delete(community);

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
