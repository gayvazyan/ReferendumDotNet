using Microsoft.AspNetCore.Mvc;
using Referendum.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IReferendumRepasitory _referendumRepasitory;
        public HomeController(IReferendumRepasitory referendumRepasitory)
        {
            _referendumRepasitory = referendumRepasitory;
        }
        [HttpPost]
        public void PostResult([FromBody] string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var referendum = _referendumRepasitory.GetByID(Convert.ToInt32(id));
                referendum.IsActive = referendum.IsActive == true ? false : true;
                _referendumRepasitory.Update(referendum);
            }

        }
    }
}
