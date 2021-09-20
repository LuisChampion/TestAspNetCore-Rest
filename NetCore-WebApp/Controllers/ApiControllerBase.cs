using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore_WebApp.Models;
using NetCore_WebApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_WebApp.Controllers
{
    public abstract class ApiControllerBase: Controller
    {
        private readonly IOptions<ApiSettingsModel> apiSettings;
        protected string apiPath;
        public ApiControllerBase(IOptions<ApiSettingsModel> apiSettings)
        {
            this.apiSettings = apiSettings;
            ApplicationSettings.WebApiUrl = apiSettings.Value.WebApiBaseUrl;
        }
    }
}
