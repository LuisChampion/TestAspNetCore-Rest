﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities;

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

        public IActionResult Create()
        {
            var listProperties = Factory.ApiClientFactory.Instance.GetAsync<List<Entities.Property>>("api/Property").Result;
            if (listProperties != null)
            {
                var selectedListItem = listProperties.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.Title,
                        Value = d.Id.ToString(),
                        Selected = false
                    };
                });
                ViewBag.PropertyItems = selectedListItem;
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Activity activity)
        {
            string apiPath = "api/Activity";
            if (ModelState.IsValid)
            {
                var resultado = await Factory.ApiClientFactory.Instance.PostAsync(apiPath, activity);
                if (resultado != null)
                {
                    if (resultado.IsSuccess == true)
                    {
                        TempData["mensaje"] = "La actividad se ha creado correctamente";
                    }
                    else
                    {
                        TempData["mensaje"] = resultado.ReturnMessage;
                    }

                }
                else
                {
                    TempData["mensaje"] = "Ocurrió un error inesperado";
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        
    }
}