using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;
using Referendum.core.Models;

namespace Referendum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly IWebService _webService;

        public IndexModel(ICitizenRepasitory citizenRepasitory,
                          IReferendumRepasitory referendumRepasitory,
                          IWebService webService)
        {
            _citizenRepasitory = citizenRepasitory;
            _referendumRepasitory = referendumRepasitory;
            _webService = webService;
            Citizen = new CitizenWebServiceResponse();
            ReferendumList = new List<ReferendumDb>();
            ReferendumModel = new ReferendumDb();
        }

        [BindProperty]
        public CitizenWebServiceResponse Citizen { get; set; }

        [BindProperty]
        public List<ReferendumDb> ReferendumList { get; set; }

        [BindProperty]
        public ReferendumDb ReferendumModel { get; set; }
        [BindProperty]
        public bool Show { get; set; }

        [BindProperty]
        public bool Connect { get; set; }
        [BindProperty]
        public string DataString { get; set; }
        [BindProperty]
        public int ReferendumId { get; set; }

        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public void PrepareData()
        {
            Citizen = _webService.GetCitizen(DataString);
            if (Citizen != null)
            {
                ReferendumList = _referendumRepasitory.GetAll().ToList();
                Show = true;
            }
        }
        public void OnGet()
        {

        }

        public ActionResult OnPost()
        {
            if (Show && Connect)
            {
                CitizenDb citizen = new CitizenDb
                {
                    FirstName = Citizen.First_name,
                    LastName = Citizen.Last_name,
                    Opaque = Citizen.Opaque,
                    Ssn = Citizen.Ssn,
                    Time = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                    ReferendumId = ReferendumId

                };
                var result = _citizenRepasitory.Insert(citizen);
                if (result != null)
                {
                    return Redirect("/Connected/" + citizen.Opaque);
                }
            }

            else
            {
                if (!Show)
                {
                    PrepareData();
                }
                else
                {
                    var connectedCitizen = _citizenRepasitory.GetAll().Where(p => p.ReferendumId == ReferendumId).FirstOrDefault(p => p.Ssn == Citizen.Ssn);

                    if (connectedCitizen != null)
                    {
                        Errors.Add(new ServiceError { Code = "ԿՐԿՆԱԿԻ ՄԻԱՑՄԱՆ ՓՈՐՁ", Description = "Քաղաքացի " + connectedCitizen.LastName + " " + connectedCitizen.FirstName + " Դուք մասնակցել եք ստորագրությունների առցանց հավաքմանը` " + connectedCitizen.Time + " - ին։" + " Ձեր ստուգման կոդը՝ " + connectedCitizen.Opaque });
                        PrepareData();
                        return Page();

                    }
                    else
                    {
                        Connect = true;
                        ReferendumModel = _referendumRepasitory.GetByID(ReferendumId);
                    }
                }
            }


            

            return Page();
        }
    }
}
