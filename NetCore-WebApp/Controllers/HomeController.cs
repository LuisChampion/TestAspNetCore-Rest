using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCore_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using NetCore_WebApp.Utility;

namespace NetCore_WebApp.Controllers
{
    public class HomeController : ApiControllerBase
    {
        //private readonly IOptions<ApiSettingsModel> apiSettings;

        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(IOptions<ApiSettingsModel> apiSettings):base(apiSettings)
        {   
        }

        public IActionResult Index()
        {
            //var listaPropiedades = Factory.ApiClientFactory.Instance.GetAsync<List<Property>>("api/Property").Result;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
