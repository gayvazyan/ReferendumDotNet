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
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<CommunitiesDb> CommunitiyList { get; set; }

        public bool ShowPrevious => CurrentPage > 1;
     //   public bool ShowCurrent => CurrentPage == CurrentPage;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public void OnGet()
        {
           var communitiyList = _communityRepasitory.GetAll().ToList();

            CommunitiyList = _communityRepasitory.GetPaginatedResult(communitiyList, CurrentPage, PageSize);
            Count = _communityRepasitory.GetCount(communitiyList);
        }

        public ActionResult OnPost()
        {
            return Page();
        }
    }
}
