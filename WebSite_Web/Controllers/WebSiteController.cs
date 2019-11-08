using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSite.Model;
using WebSite.Services;
using WebSite_Repositories.Entities;
using WebSite_Web.Models;


namespace WebSite_Web.Controllers
{
   
    public class WebSiteController : Controller
    {
        private readonly IWebSiteServices _webSiteServices;
        public WebSiteController(IWebSiteServices webSiteServices)
        {
            _webSiteServices = webSiteServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            (bool success, List<Company> output) = await _webSiteServices.GetCompanies();
            if (success)
                return View(output);
            else
            {

                var NotFoundViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                return View("Error", NotFoundViewModel);
            }

        }
        [HttpGet]
        public async Task<IActionResult>  GetReportByParam([FromQuery] QueryParameters request)
        {

            (bool success, List<Report> output) = await _webSiteServices.GetReportByParam(request.CompanyId, request.StartDate, request.EndDate);
            if (success)
                return View(output);
            else
            {
                var NotFoundViewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                return View("Error", NotFoundViewModel);
            }

        }
        [Route("/")]
        [HttpGet]
        public async Task<ActionResult<List<Bank>>> GetBanks()
        {
            (bool success, List<Bank> output) = await _webSiteServices.GetBanks();
            if (success)
                return output;
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}