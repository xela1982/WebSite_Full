
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSite_Repositories.Entities;
using WebSite_Repositories.Services;

namespace WebSite.Services
{
    public class WebSiteService : IWebSiteServices
    {
        private readonly IWebSiteRepositories _repositoryService;
        public WebSiteService(IWebSiteRepositories repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<bool> DeleteBank(int id)
        {
            try
            {
                return  await _repositoryService.DeleteBank(id);

            }
            catch (Exception ex)
            {
                //TODO LOG Exception
                return (false);
            }
        }

        public async Task<(bool success, Bank output)> GetBank(int id)
        {
            try
            {
                var output = await _repositoryService.GetBank(id);
                return (true, output);
            }
            catch (Exception ex)
            {
                //TODO LOG Exception
                return (false, null);
            }
        }

        public async Task<(bool, List<Bank>,Exception)> GetBanks()
        {
            try
            {
                var output = await _repositoryService.GetBanks();
                return (true, output,null);
            }
            catch (Exception ex)
            {
                ex.Source = "_repositoryService.GetBanks";
                //TODO LOG Exception
                return (false, null, ex);
            }
        }

        public async Task<(bool, List<Company>)> GetCompanies()
        {

            try
            {
                var output = await _repositoryService.GetCompanies();
                return (true, output);
            }
            catch (Exception ex)
            {
                //TODO LOG Exception
                return (false, null);
            }

        }
        public async Task<(bool, List<Report>)> GetReportByParam(int companyId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var output = await _repositoryService.GetReportByParam(companyId, startDate, endDate);
                return (true, output);
            }
            catch (Exception ex)
            {
                //TODO LOG Exception
                return (false, null);
            }


        }

        public async Task<(bool, int, Exception)> InsertBank(string name)
        {
            try
            {
                var output = await _repositoryService.InsertBank(name);
                return (true, output,null);
            }
            catch (Exception ex)
            {
                ex.Source = "_repositoryService.InsertBank";
                //TODO LOG Exception
                return (false, 0, ex);
            }
        }

        public async Task<bool> UpdateBank(int id, string name)
        {
            try
            {
                return await _repositoryService.UpdateBank(id,name);

            }
            catch (Exception ex)
            {
                //TODO LOG Exception
                return (false);
            }
        }
    }
}
