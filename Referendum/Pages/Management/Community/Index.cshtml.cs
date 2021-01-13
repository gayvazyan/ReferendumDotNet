using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.Management.Community
{
    public class IndexModel : PageModel
    {
        private readonly ICommunityRepasitory _communityRepasitory;
        public IndexModel(ICommunityRepasitory communityRepasitory)
        {
            _communityRepasitory = communityRepasitory;
            Input = new InputModel();
            InputList = new List<InputModel>();
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public List<InputModel> InputList = new List<InputModel>();
        public class InputModel : CommunitiesDb
        {
           
        }

        //START Part Paging
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<CommunitiesDb> CommunitiyList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;
        //END Part Paging


        protected void PrepareData()
        {
            var communitiyList = _communityRepasitory.GetAll().ToList();

            if (Input.CommunityCode!=null )
            {
                communitiyList = communitiyList.Where(p=>p.CommunityCode.Contains(Input.CommunityCode)).ToList();
            }

            if (Input.CommunityName != null)
            {
                communitiyList = communitiyList.Where(p => p.CommunityName.ToUpper().Contains(Input.CommunityName.ToUpper())).ToList();
            }
           

            CommunitiyList = _communityRepasitory.GetPaginatedResult(communitiyList, CurrentPage, PageSize);
            Count = _communityRepasitory.GetCount(communitiyList);

            InputList = CommunitiyList.Select(p =>
            {
                return new InputModel()
                {
                    Id = p.Id,
                    CommunityCode = p.CommunityCode,
                    CommunityName = p.CommunityName
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
