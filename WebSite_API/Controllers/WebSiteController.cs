using WebSite.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using WebSite_Repositories.Entities;
using WebSite.Services;

namespace WebSite.Controllers
{
    [Route("/api/[action]")]
    [ApiController]
    public class WebSiteController : ControllerBase
    {

        private readonly IWebSiteServices _webSiteServices;
        public WebSiteController(IWebSiteServices webSiteServices)
        {
            _webSiteServices = webSiteServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            (bool success, List<Company> output) = await _webSiteServices.GetCompanies();
            if (success)
                return output;
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }
       
        [HttpGet]
        public async Task<ActionResult<List<Report>>> GetReportByParam([FromQuery] QueryParameters request)
        {
            (bool success, List<Report> output) = await _webSiteServices.GetReportByParam(request.CompanyId, request.StartDate, request.EndDate);
            if (success)
                return output;
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }
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
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            (bool success, Bank output) = await _webSiteServices.GetBank(id);
            if (success)
                return output;
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}