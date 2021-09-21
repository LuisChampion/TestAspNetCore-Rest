using Microsoft.AspNetCore.Mvc;
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
            return View(listActivities?.OrderBy(o=> o.Schedule)?.ToList());
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

        
        [HttpPost("{id}"), ActionName("Cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            string apiPath = $"api/Activity/Cancel/{id}";
            if (ModelState.IsValid)
            {
                var resultado = await Factory.ApiClientFactory.Instance.PutAsync<Activity>(apiPath, null);
                if (resultado != null)
                {
                    TempData["mensaje"] = resultado.ReturnMessage;
                }
                else
                {
                    TempData["mensaje"] = "Ocurrió un error inesperado";
                }
                return RedirectToAction("Index");
            }
            return View();
        }


        
        public async Task<IActionResult> EditReagenda(int id)
        {
           
            string apiPath = $"api/Activity/{id}";
            if ( id == 0)
            {
                return NotFound();
            }

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

            var actividad = await Factory.ApiClientFactory.Instance.GetAsync<Activity>(apiPath);
            if (actividad== null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reagenda(Activity activity)
        {
            //var actividad = await Factory.ApiClientFactory.Instance.GetAsync<Activity>($"api/Activity/{activity.Id}");
            //actividad.Schedule = activity.Schedule;
            var resultado = await Factory.ApiClientFactory.Instance.PostAsync("api/Activity/Reagenda", activity);
                if (resultado != null)
                {
                    TempData["mensaje"] = resultado.ReturnMessage;
                }
                else
                {
                    TempData["mensaje"] = "Ocurrió un error inesperado";
                    
                }
            return RedirectToAction("Index");
        }

    }
}
