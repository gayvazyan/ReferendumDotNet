using Referendum.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core
{
   public interface IWebService
    {
        CitizenWebServiceResponse GetCitizen(string data);
        Task<CitizenWebServiceResponse> GetCitizenById();
        string GetPath();
    }
}
