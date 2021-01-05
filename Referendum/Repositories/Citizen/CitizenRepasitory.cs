using Referendum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.Repositories
{
    public class CitizenRepasitory : Repositories<Table1>, ICitizenRepasitory
    {
        public CitizenRepasitory(ReferendumContext dbContext) : base(dbContext) { }
    }
}
