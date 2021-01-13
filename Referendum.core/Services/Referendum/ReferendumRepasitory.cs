using Referendum.core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Referendum.core
{
    public class ReferendumRepasitory : Repositories<ReferendumDb>, IReferendumRepasitory
    {
        public ReferendumRepasitory(ReferendumContext dbContext) : base(dbContext) { }

        public List<ReferendumDb> GetPaginatedResult(List<ReferendumDb> data, int currentPage, int pageSize = 10)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<ReferendumDb> data)
        {
            return data.Count;
        }
    }
}
