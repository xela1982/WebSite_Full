using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSite.Services;
using WebSite_Repositories.Entities;
using WebSite_Web.Models;
namespace WebSite.Controllers
{
  
    //[ApiController]
    public class TempWebSiteController : Controller
    {
        private readonly IWebSiteServices _webSiteServices;
        public TempWebSiteController(IWebSiteServices webSiteServices)
        {
            _webSiteServices = webSiteServices;
        }
        [Route("/")]
        [Route("/[action]")]
        [HttpGet]
        public async Task<ActionResult<List<Bank>>> GetBanks()
        {
           
            (bool success, List<Bank> output, Exception ex) = await _webSiteServices.GetBanks();
            if (success)
                return View(output);
            else
            {
                var NotFoundViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ExceptionResponse = ex };
                return View("Error", NotFoundViewModel);
            }
        }
        [Route("/[action]")]
        [HttpPost]
        public async Task<ActionResult<List<Bank>>> PostBanks(string name)
        {
            if (name == string.Empty || name==null)
                return RedirectToAction("GetBanks");

            (bool success, int output, Exception ex) = await _webSiteServices.InsertBank(name);
            if (success)
                return RedirectToAction("GetBanks");
            else
            {

                var NotFoundViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier , ExceptionResponse = ex};
                return View("Error", NotFoundViewModel);
            }
        }
        [Route("/[action]")]
        [HttpGet]
        public async Task<ActionResult> DeleteBank(int id)
        {
            bool success = await _webSiteServices.DeleteBank(id);
            if (success)
                return RedirectToAction("GetBanks");
            else
            {
                var NotFoundViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                return View("Error", NotFoundViewModel);
            }
        }
        [Route("/[action]")]
        [HttpGet]
        public async Task<ActionResult<Bank>> EditBank(int id)
        {
            (bool success, Bank output) = await _webSiteServices.GetBank(id);
            if (success)
                return View(output);
            else
            {
                var NotFoundViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                return View("Error", NotFoundViewModel);
            }

        }
        [Route("/[action]")]
        [HttpPost]
        public async Task<ActionResult> UpdateBank(int Id, string Name)
        {

            bool success = await _webSiteServices.UpdateBank(Id, Name);
            if (success)
                return RedirectToAction("GetBanks");
            else
            {
                var NotFoundViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                return View("Error", NotFoundViewModel);
            }
        }
        
    }
}