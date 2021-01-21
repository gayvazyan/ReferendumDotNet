using Referendum.core.Entities;
using System.Collections.Generic;

namespace Referendum.core
{
    public interface IReferendumRepasitory : IRepositories<ReferendumDb> 
    {
        List<ReferendumDb> GetPaginatedResult(List<ReferendumDb> data, int currentPage, int pageSize);
        int GetCount(List<ReferendumDb> data);
    }
}
