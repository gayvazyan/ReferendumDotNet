using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;

namespace Referendum.Pages
{
    public class CheckModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        private readonly IReferendumRepasitory _referendumRepasitory;
        public CheckModel(ICitizenRepasitory citizenRepasitory,
                          IReferendumRepasitory referendumRepasitory)
        {
            _citizenRepasitory = citizenRepasitory;
            _referendumRepasitory = referendumRepasitory;
            Info = new InfoModel();
        }

        [BindProperty]

        [Required(ErrorMessage ="Լրացրեք գրանցման կոդը")]
        public string CheckCode { get; set; }

        public class InfoModel
        {
            public string CitizenFirstName { get; set; }
            public string CitizenLastName { get; set; }
            public string CitizenSsn { get; set; }
            public string Date { get; set; }
            public string ReferendumQuestion { get; set; }
            
        }

        [BindProperty]
        public InfoModel Info { get; set; }


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
                    var citizen = _citizenRepasitory.GetAll().FirstOrDefault(p => p.Opaque == CheckCode);
                    if (citizen!=null)
                    {
                        Info.CitizenFirstName = citizen.FirstName;
                        Info.CitizenLastName = citizen.LastName;
                        Info.CitizenSsn = citizen.Ssn;
                        Info.Date = citizen.Time;

                        var referendum = _referendumRepasitory.GetAll().FirstOrDefault(p => p.Id == citizen.ReferendumId);

                        Info.ReferendumQuestion = referendum.Question;
                    }
                    else
                    {
                        Errors.Add(new ServiceError { Code = "Սխալ", Description = "Մուտքագրված գրանցման կոդով տվյալ չի գտնվել։" });
                        CheckCode = string.Empty;
                        return Page();
                    }
                   
                    
                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "001", Description = ex.Message });
                }
            }
            return Page();

        }
    }
}
