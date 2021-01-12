using Referendum.core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Referendum.core
{
    public interface ICommunityRepasitory : IRepositories<CommunitiesDb>
    {
        List<CommunitiesDb> GetPaginatedResult(List<CommunitiesDb> data, int currentPage, int pageSize = 10);
        int GetCount(List<CommunitiesDb> data);
    }
}
