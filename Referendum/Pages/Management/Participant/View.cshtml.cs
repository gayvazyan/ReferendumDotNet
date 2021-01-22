using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.Participant
{
   public class ViewModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly IWebService _webService;
        public ViewModel(ICitizenRepasitory citizenRepasitory,
                         ICommunityRepasitory communityRepasitory,
                         IReferendumRepasitory referendumRepasitory,
                         IWebService webService)
        {
            _citizenRepasitory = citizenRepasitory;
            _communityRepasitory = communityRepasitory;
            _referendumRepasitory = referendumRepasitory;
            _webService = webService;
            View = new ViewCitizenModelModel();
        }
        public class ViewCitizenModelModel : CitizenDb 
        {
            public string Question { get; set; }
            public string ImageSource { get; set; }
            public DateTime? Birthdate { get; set; }
            public bool? Gender { get; set; }
            public string MiddleName { get; set; }
        }

        [BindProperty]
        public ViewCitizenModelModel View { get; set; }

        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public void OnGet(int id)
        {
            var result = _citizenRepasitory.GetByID(id);
            var citizenBySSN = _webService.GetCitizenBySSN(result.Ssn); 

            if (result != null)
            {
                View.Id = result.Id;
                View.FirstName = result.FirstName;
                View.LastName = result.LastName;
                View.Ssn = result.Ssn;
                View.Time = result.Time;
                View.Question = _referendumRepasitory.GetAll().ToList().Find(r => r.Id == result.ReferendumId).Question;
                View.Birthdate = DateTime.ParseExact(citizenBySSN.Result.BirthDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); 
                View.Gender = (citizenBySSN.Result.Gender == "M") ? true : false;
                View.MiddleName = citizenBySSN.Result.MiddleName;
               
                byte[] data = Convert.FromBase64String(citizenBySSN.Result.Photo);
                View.ImageSource = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(data));
            }
        }

        public ActionResult OnPost()
        {
            return Page();
        }
    }
}
