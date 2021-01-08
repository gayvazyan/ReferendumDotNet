using Referendum.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core
{
   public interface IWebService
    {
        //Id card
        CitizenWebServiceResponse GetCitizen(string data);
        Task<CitizenWebServiceResponse> GetCitizenById();

        //Passport
        Task<PassportWebServiceResponse> GetCitizenByPassport(string passportNumber);

        //SSN
        Task<SSNWebServiceResponse> GetCitizenBySSN(string ssn);

        //Test Services
        Task<PassportWebServiceResponse> GetCitizenByPassportTest(string passportNumber);
        Task<SSNWebServiceResponse> GetCitizenBySSNTest(string ssn);
    }
}
