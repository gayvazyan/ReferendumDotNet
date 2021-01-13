using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.ReferendumT
{
    public class IndexModel : PageModel
    {
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;
        public IndexModel(IReferendumRepasitory referendumRepasitory, 
                          ICommunityRepasitory communityRepasitory)

        {
            _referendumRepasitory = referendumRepasitory;
            _communityRepasitory = communityRepasitory;
            Input = new InputModel();
            InputList = new List<InputModel>();
            CommunityList = new List<CommunitiesDb>();

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList = new List<InputModel>();
        public class InputModel : ReferendumDb
        {
            public string CommunityName { get; set; }
        }

        // START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 5;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<ReferendumDb> ReferendumList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        // END Part Paging


        [BindProperty]
        public string SelectedCommunityId { get; set; }
        [BindProperty]
        public List<CommunitiesDb> CommunityList { get; set; }


        protected void PrepareData()
        {
            CommunityList = _communityRepasitory.GetAll().ToList();

            var referendumList = _referendumRepasitory.GetAll().ToList();



            if (Input.Question != null)
            {
                referendumList = referendumList.Where(p => p.Question.ToUpper().Contains(Input.Question.ToUpper())).ToList();
            }

            if (Input.StartDate != null)
            {
                referendumList = referendumList.Where(p => p.StartDate >= Input.StartDate).ToList();
            }

            if (Input.EndDate != null)
            {
                referendumList = referendumList.Where(p => p.EndDate <= Input.EndDate).ToList();
            }

            if (SelectedCommunityId != null)
            {
                referendumList = referendumList.Where(p => p.CommunityId==Convert.ToInt32(SelectedCommunityId)).ToList();
            }


            ReferendumList = _referendumRepasitory.GetPaginatedResult(referendumList, CurrentPage, PageSize);
            Count = _referendumRepasitory.GetCount(referendumList);

            InputList = ReferendumList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    Question = p.Question,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    ConnectionCount = p.ConnectionCount,
                    IsActive = p.IsActive,
                    CommunityId = p.CommunityId,
                    CommunityName = (p.CommunityId!=null) ?  _communityRepasitory.GetAll().FirstOrDefault(f=>f.Id==p.CommunityId).CommunityName : "Համապետական հանրաքվե"
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
