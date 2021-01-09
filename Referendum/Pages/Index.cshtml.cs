using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;
using Referendum.core.Models;
using Referendum.core.Common;

namespace Referendum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;
        
        private readonly IWebService _webService;

        public IndexModel(ICitizenRepasitory citizenRepasitory,
                          IReferendumRepasitory referendumRepasitory,
                          ICommunityRepasitory communityRepasitory,
                          IWebService webService)
        {
            _citizenRepasitory = citizenRepasitory;
            _referendumRepasitory = referendumRepasitory;
            _communityRepasitory = communityRepasitory;
            _webService = webService;
            Citizen = new CitizenWebServiceResponse();
            ReferendumList = new List<ReferendumDb>();
            ReferendumModel = new ReferendumDb();
            Result = new WebServiceResponse();
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
        public WebServiceResponse Result { get; set; }

        [BindProperty]
        public string OpaqueUniqueID { get; set; }

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
            if (Result.Status=="OK")
            {
                Citizen = _webService.GetCitizen(Result.Data);
                if (Citizen != null)
                {
                    ReferendumList = _referendumRepasitory.GetAll().ToList();
                    Show = true;
                }
            }
            else
            {
                Errors.Add(new ServiceError { Code = "001", Description = Result.Message });
            }
        }
        public void OnGet()
        {
            OpaqueUniqueID = UniqueID.GetUniqueID();
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
                var resultCitizen = _citizenRepasitory.Insert(citizen);
                if (resultCitizen != null)
                {
                    var referendum = _referendumRepasitory.GetByID((int)citizen.ReferendumId);
                    if (referendum.ConnectionCount==null)
                    {
                        referendum.ConnectionCount = 1;
                    }
                    else
                    {
                        referendum.ConnectionCount ++;
                    }
                  
                   _referendumRepasitory.Update(referendum);

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
                        ReferendumModel = _referendumRepasitory.GetByID(ReferendumId);

                        var communityNameBPR = _webService.GetCitizenBySSNTest(Citizen.Ssn).Result.Community;

                        var communityIdBPR = _communityRepasitory.GetAll().FirstOrDefault(p => p.CommunityName == communityNameBPR).Id;
                        
                        if (communityIdBPR != ReferendumModel.CommunityId && ReferendumModel.CommunityId != null)
                        {
                            var communityNameRef = _communityRepasitory.GetAll().FirstOrDefault(p => p.Id == ReferendumModel.CommunityId).CommunityName;

                            Errors.Add(new ServiceError { Code = "Սխալ", Description = "Քաղաքացի " + Citizen.Last_name + " " + Citizen.First_name + " Դուք հաշվառված էք  " + communityNameBPR + " համայնքում։ Սակայն հանրաքվեի նախաձեռնությանը որին դուք ցանկանում էք միանալ տեղի է ունենում " + communityNameRef + " համայնքում , որին Դուք իրավունք չունեք մասնակցել։" });
                            PrepareData();
                            return Page();
                        }
                        else
                        {
                            Connect = true;
                        }
                    }
                }
            }
            return Page();
        }
    }
}
