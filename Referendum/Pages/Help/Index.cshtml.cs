using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Help
{

    public class IndexModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        public IndexModel(ICitizenRepasitory citizenRepasitory)
        {
            _citizenRepasitory = citizenRepasitory;
        }
        public void OnGet()
        {
            // CitizenDb obje = new CitizenDb { Id = 1, FirstName = "Garegin", LastName = "Այվազյան", Opaque = "sfsfsf", Ssn = "0985455555", Time = "12.03.2020" };
            // _citizenRepasitory.Insert(obje);
            var newobe = _citizenRepasitory.GetByID(1);
            _citizenRepasitory.Delete(newobe);
        }

        public void OnPost()
        {
        }
    }
}
