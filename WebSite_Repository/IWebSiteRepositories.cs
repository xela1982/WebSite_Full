using WebSite_Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebSite_Repositories.Services
{
    public interface IWebSiteRepositories
    {
        Task<List<Report>> GetReportByParam(int companyId, DateTime startDate, DateTime endDate);
        Task<List<Company>> GetCompanies();
        Task<List<Bank>> GetBanks();
        Task<int> InsertBank(string name);
        Task<Bank> GetBank(int id);
        Task<bool> DeleteBank(int id);
        Task<bool> UpdateBank(int id, string name);
    }
}
