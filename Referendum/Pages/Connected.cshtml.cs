using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages
{
    public class ConnectedModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        private readonly IReferendumRepasitory _referendumRepasitory;
        public ConnectedModel(ICitizenRepasitory citizenRepasitory,
                          IReferendumRepasitory referendumRepasitory)
        {
            _citizenRepasitory = citizenRepasitory;
            _referendumRepasitory = referendumRepasitory;
            Citizen = new CitizenDb();
        }
        [BindProperty]
        public CitizenDb Citizen { get; set; }
        public void OnGet(string id)
        {
            Citizen = _citizenRepasitory.GetAll().FirstOrDefault(p => p.Opaque == id);
        }
    }
}
