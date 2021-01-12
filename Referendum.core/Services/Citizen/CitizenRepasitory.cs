using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Referendum.core.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core
{
    public class CitizenRepasitory : Repositories<CitizenDb>, ICitizenRepasitory
    {
        public CitizenRepasitory(ReferendumContext dbContext) : base(dbContext) 

        {
           
        }
    }
}
