using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_WebApp.Controllers
{
    public class ActivitiesController : ApiControllerBase
    {
        public ActivitiesController(Microsoft.Extensions.Options.IOptions<Models.ApiSettingsModel> apiSettings) :base(apiSettings)
        {
            
        }
        public async Task<IActionResult> Index()
        {
            string apiPath = "api/Activity";
            var listActivities = await Factory.ApiClientFactory.Instance.GetAsync<List<Entities.Activity>>(apiPath);
            return View(listActivities);
        }
    }
}
