using Referendum.core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Referendum.core
{
    public interface ICitizenRepasitory : IRepositories<CitizenDb>
    
    {
        List<CitizenDb> GetPaginatedResult(List<CitizenDb> data, int currentPage, int pageSize);
        int GetCount(List<CitizenDb> data);
    }

   
}
