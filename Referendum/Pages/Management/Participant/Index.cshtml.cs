using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.Participant
{
    public class IndexModel : PageModel
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;
        private readonly IReferendumRepasitory _referendumRepasitory;
        public IndexModel(ICitizenRepasitory citizenRepasitory,
                          ICommunityRepasitory communityRepasitory,
                          IReferendumRepasitory referendumRepasitory)
        {
            _citizenRepasitory = citizenRepasitory;
            _communityRepasitory = communityRepasitory;
            _referendumRepasitory = referendumRepasitory;
            Input = new InputModel();
            InputList = new List<InputModel>();
            SelectReferendumList = new List<ReferendumDb>();
        }


        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public List<ReferendumDb> SelectReferendumList { get; set; }

        public List<InputModel> InputList = new List<InputModel>();
        public class InputModel : CitizenDb
        {
            public string Question { get; set; }
        }

        //START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 15;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<CitizenDb> CitizenList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        //END Part Paging


        protected void PrepareData()
        {
            var citizenList = _citizenRepasitory.GetAll().ToList();
            var referendumList =_referendumRepasitory.GetAll().ToList();

            foreach (var item in referendumList)
            {
                ReferendumDb itemReferendum = new ReferendumDb() { Id = item.Id, Question = item.Question };
                SelectReferendumList.Add(itemReferendum);
            }

            if (Input.ReferendumId != null)
            {
                citizenList = citizenList.Where(p => p.ReferendumId==Input.ReferendumId).ToList();
            }


            CitizenList = _citizenRepasitory.GetPaginatedResult(citizenList, CurrentPage, PageSize);
            Count = _citizenRepasitory.GetCount(citizenList);

            InputList = CitizenList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Ssn = p.Ssn,
                    Time = p.Time,
                    Question = _referendumRepasitory.GetAll().ToList().FirstOrDefault(r => r.Id == p.ReferendumId).Question
                };
            }).ToList();
        }
        public void OnGet()
        {
            PrepareData();
        }

        public ActionResult OnPost()
        {
            PrepareData();
            return Page();
        }
    }
}
