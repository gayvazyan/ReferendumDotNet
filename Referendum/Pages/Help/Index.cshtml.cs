using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.Model;
using Referendum.Repositories;

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
            Table1 obje = new Table1 { Id = 1, Hhh = "GGG", Hhhgg = "AAA" };
            var newobe = _citizenRepasitory.GetByID(1);
            _citizenRepasitory.Delete(newobe);
        }
           
        public void OnPost()
        {
                Table1 obje = new Table1 { Id = 1, Hhh = "GGG", Hhhgg = "AAA" };
            }
    }
}
