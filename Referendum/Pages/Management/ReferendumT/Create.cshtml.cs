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
    public class CreateModel : PageModel
    {
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;
        

        public CreateModel(IReferendumRepasitory referendumRepasitory,
                           ICommunityRepasitory communityRepasitory)
        {
            _referendumRepasitory = referendumRepasitory;
            _communityRepasitory = communityRepasitory;
            Create = new CreateReferendumModel();
            CommunityList = new List<CommunitiesDb>();
        }

        public class CreateReferendumModel : ReferendumDb { }

        [BindProperty]
        public CreateReferendumModel Create { get; set; }

        [BindProperty]
        public bool IsActive { get; set; }

        [BindProperty]
        public string SelectedCommunityId { get; set; }

        [BindProperty]
        public List<CommunitiesDb> CommunityList { get; set; }

        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public void OnGet()
        {
            CommunityList = _communityRepasitory.GetAll().ToList();
        }
        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var referendum = new ReferendumDb
                    {
                        Id = Create.Id,
                        Question = Create.Question,
                        StartDate = Create.StartDate,
                        EndDate = Create.EndDate,
                        IsActive = IsActive,
                        CommunityId = !String.IsNullOrEmpty(SelectedCommunityId) ? (int?)Convert.ToInt32(SelectedCommunityId) : null

                    };

                    var result = _referendumRepasitory.Insert(referendum);

                    if (result != null)
                        return RedirectToPage("/Management/ReferendumT/Index");

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
