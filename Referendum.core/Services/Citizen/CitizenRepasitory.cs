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
        public CitizenRepasitory(ReferendumContext dbContext) : base(dbContext)  { }

        public List<CitizenDb> GetPaginatedResult(List<CitizenDb> data, int currentPage, int pageSize)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<CitizenDb> data)
        {
            return data.Count;
        }
    }
}
