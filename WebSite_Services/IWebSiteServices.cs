
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSite_Repositories.Entities;

namespace WebSite.Services
{
    public interface IWebSiteServices
    {
        Task<(bool,List<Report>)> GetReportByParam(int companyId, DateTime startDate, DateTime endDate);
        Task<(bool,List<Company>)> GetCompanies();
        Task<(bool, List<Bank>)> GetBanks();
        Task<(bool success, Bank output)> GetBank(int id);
        Task<(bool,int)> InsertBank(string name);
        Task<bool> DeleteBank(int id);
        Task<bool> UpdateBank(int id, string name);
    }
}
