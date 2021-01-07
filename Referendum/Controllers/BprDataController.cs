using Microsoft.AspNetCore.Mvc;
using Referendum.core;
using System;
using System.Collections.Generic;

namespace Referendum.Controllers
{
    [Route("[controller]/[action]")]
    public class BprDataController : Controller
    {
        private readonly IWebService _webService;
        public BprDataController(IWebService webService)
        {
            _webService = webService;
        }

        [HttpPost]
        public void BprResult([FromBody] string data)
        {
            //try
            //{
            //    foreach (var item in data)
            //    {
                   
            //    }
            //    return "OK";
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }
    }
}
