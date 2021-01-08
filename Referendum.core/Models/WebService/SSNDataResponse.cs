using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.core.Models
{
    public class SSNDataResponse
    {
        public string Opaque { get; set; }

        public string Last_name { get; set; }

        public string First_name { get; set; }

        public string SSN { get; set; }

        public string E_civil { get; set; }

        public string Vehicle_info { get; set; }

        public string Driving_license { get; set; }

        public string E_register { get; set; }

        public string Ces_data { get; set; }

        public PassportData Passport_data { get; set; }

    }

    public class PassportData
    {
        public string PNum { get; set; }
        public string SSNIndicator { get; set; }
        public string Photo { get; set; }
        public string IsDead { get; set; }

        public AVVRegistrationAddress AVVRegistrationAddress { get; set; }
        public AVVDocuments AVVDocuments { get; set; }
    }

    public class AVVRegistrationAddress
    {
        public string LocationCode { get; set; }
        public string Region { get; set; }
        public string Community { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string BuildingType { get; set; }
        public string Apartment { get; set; }
    }

    public class AVVDocuments
    {
        public AVVDocumentItems AVVDocument { get; set; }
    }

    public class AVVDocumentItems
    {
        public string DocumentDepartment { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string EnglishLastName { get; set; }
        public string EnglishFirstName { get; set; }
        public string EnglishMiddleName { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public string IssuanceDate { get; set; }
        public string ValidityDate { get; set; }
        public DocumentIdentifier DocumentIdentifier { get; set; }
        public Citizenship Citizenship { get; set; }

    }
    public class DocumentIdentifier
    {
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
    }
    public class Citizenship
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}
