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
    public class DeleteModel : PageModel
    {
        private readonly IReferendumRepasitory _referendumRepasitory;
        public DeleteModel(IReferendumRepasitory referendumRepasitory)
        {
            _referendumRepasitory = referendumRepasitory;
            Delete = new DeleteReferendumModel();
        }

        public class DeleteReferendumModel : ReferendumDb { }

        [BindProperty]
        public DeleteReferendumModel Delete { get; set; }

      
        
        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }
        public void OnGet(int id)
        {
            var result = _referendumRepasitory.GetByID(id);

            if (result != null)
            {
                Delete.Id = result.Id;
                Delete.Question = result.Question;
                Delete.StartDate = result.StartDate;
                Delete.EndDate = result.EndDate;
                Delete.Question = result.Question;
            }
        }
        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var referendum = _referendumRepasitory.GetByID(Delete.Id);

                    _referendumRepasitory.Delete(referendum);

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
