using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Referendum.core;
using Referendum.core.Entities;

namespace Referendum.Pages.admin
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepasitory _userRepasitory;
        public LoginModel(IUserRepasitory userRepasitory,
                          IHttpContextAccessor httpContextAccessor)

        {
            _httpContextAccessor = httpContextAccessor;
            _userRepasitory = userRepasitory;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : UserDb { }

        private List<ServiceError> _errors;
        public List<ServiceError> Errors
        {
            get => _errors ?? (_errors = new List<ServiceError>());
            set => _errors = value;
        }

        public void OnGet()
        {
           // _httpContextAccessor.HttpContext.Session.SetString("user", "login");
        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var userList = _userRepasitory.GetAll().ToList();
                    var user = new UserDb
                    {
                        Login = Input.Login,
                        Password = Input.Password,
                    };

                    foreach (var item in userList)
                    {
                        if (String.Equals(user.Login, item.Login) && String.Equals(user.Password , item.Password))
                           
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("user", "login");
                        }
                    }

                    if (_httpContextAccessor.HttpContext.Session.GetString("user") == "login")
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        Errors.Add(new ServiceError { Code = "Մուտքի սխալ", Description = "Մուտքագրված տվյալներով օգտատեր չի գտնվել։" });
                        return Page();
                    }



                }
                catch (Exception ex)
                {
                    Errors.Add(new ServiceError { Code = "005", Description = ex.Message });
                }
            }
            return Page();
        }
    }
}
