using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.Citizen
{
    public class IndexModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        public IndexModel(ICitizenRepasitory citizenRepasitory)
        {
            _citizenRepasitory = citizenRepasitory;
            InputList = new List<InputModel>();
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList = new List<InputModel>();
        public class InputModel : CitizenDb { }


        protected void PrepareData()
        {
            var citizenList = _citizenRepasitory.GetAll().ToList();

            InputList = citizenList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    Opaque = p.Opaque,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    ReferendumId = p.ReferendumId,
                    Ssn = p.Ssn,
                    Time = p.Time,
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
