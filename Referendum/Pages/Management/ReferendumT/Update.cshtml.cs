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
    public class UpdateModel : PageModel
    {
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;
        public UpdateModel(IReferendumRepasitory referendumRepasitory,
                           ICommunityRepasitory communityRepasitory)
        {
            _referendumRepasitory = referendumRepasitory;
            _communityRepasitory = communityRepasitory;
            Update = new UpdateReferendumModel();
            CommunityList = new List<CommunitiesDb>();
        }

        public class UpdateReferendumModel : ReferendumDb { }

        [BindProperty]
        public UpdateReferendumModel Update { get; set; }

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

        public void OnGet(int id)
        {
            CommunityList = _communityRepasitory.GetAll().ToList();
            var result =  _referendumRepasitory.GetByID(id);

            if (result != null)
            {
                Update.Id = result.Id;
                Update.Question = result.Question;
                Update.StartDate = result.StartDate;
                Update.EndDate = result.EndDate;
                Update.ConnectionCount = result.ConnectionCount;
                SelectedCommunityId = result.CommunityId.ToString();
                IsActive=(bool)result.IsActive;
                //Update.CommunityId = result.CommunityId;
                //Update.IsActive = result.IsActive;
            }
        }

        public ActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var referendum = _referendumRepasitory.GetByID(Update.Id);

                    referendum.Question = Update.Question;
                    referendum.StartDate = Update.StartDate;
                    referendum.EndDate = Update.EndDate;
                    referendum.CommunityId = !String.IsNullOrEmpty(SelectedCommunityId) ? (int?)Convert.ToInt32(SelectedCommunityId) : null;
                    referendum.ConnectionCount = Update.ConnectionCount;
                    referendum.IsActive = (bool?)IsActive;

                     _referendumRepasitory.Update(referendum);
                   
                    return RedirectToPage("/Management/ReferendumT/Index");
                   
                }
                catch (Exception ex)
                {
                    CommunityList = _communityRepasitory.GetAll().ToList();
                    Errors.Add(new ServiceError { Code = "005", Description = ex.Message });
                }
            }

            return Page();
        }
    }
}
