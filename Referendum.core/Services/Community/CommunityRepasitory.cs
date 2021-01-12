using Microsoft.EntityFrameworkCore;
using Referendum.core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core
{
    public class CommunityRepasitory : Repositories<CommunitiesDb>, ICommunityRepasitory
    {
        public CommunityRepasitory(ReferendumContext dbContext) : base(dbContext) { }
        public List<CommunitiesDb> GetPaginatedResult(List<CommunitiesDb> data, int currentPage, int pageSize = 10)
        {
            return data.OrderBy(d => d.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetCount(List<CommunitiesDb> data)
        {
            return data.Count;
        }
    }
}
