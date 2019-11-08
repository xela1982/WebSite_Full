


using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebSite_Repositories.Entities;
namespace WebSite_Repositories.Services
{

    public class ADORepository : IWebSiteRepositories
    {

        private IConfiguration _config;
        public string ConnectionString;
        public ADORepository(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config.GetSection("ConnectionStrings:DevConnection").Value;
        }
        public async Task<List<Company>> GetCompanies()
        {
            var output = new List<Company>();
            var cn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand("sp_GetCompanies", cn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var ds = new DataSet();
            var da = new SqlDataAdapter(cmd);
            try
            {
                await cn.OpenAsync();
                da.Fill(ds);
                output.AddRange(from DataRow dr in ds.Tables[0].Rows
                                select new Company
                                {
                                    CompanyId = Convert.ToInt32(dr["company_id"]),
                                    CompanyName = Convert.ToString(dr["company_name"])

                                });

            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                da.Dispose();
                ds.Dispose();
                cmd.Dispose();
                cn.Dispose();
            }
            return output;
        }
        public async Task<List<Report>> GetReportByParam(int companyId, DateTime startDate, DateTime endDate)
        {
            var output = new List<Report>();
            var cn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand("sp_EmployeesCardsReport", cn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@company_id", companyId);
            cmd.Parameters.AddWithValue("@start_date", startDate);
            cmd.Parameters.AddWithValue("@end_date", endDate);
            var ds = new DataSet();
            var da = new SqlDataAdapter(cmd);
            try
            {
                await cn.OpenAsync();
                da.Fill(ds);
                output.AddRange(from DataRow dr in ds.Tables[0].Rows
                                select new Report
                                {
                                    UserId = Convert.ToInt32(dr["user_id"]),
                                    UserFullName = Convert.ToString(dr["full_name"]),
                                    Last4Digits = Convert.ToInt32(dr["last_digit"]),
                                    Payment = Convert.ToDouble(dr["payment_sum"]),
                                });

            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                da.Dispose();
                ds.Dispose();
                cmd.Dispose();
                cn.Dispose();
            }
            return output;
        }

        public async Task<List<Bank>> GetBanks()
        {
            var output = new List<Bank>();
            var cn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand("sp_GetBanks", cn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var ds = new DataSet();
            var da = new SqlDataAdapter(cmd);
            try
            {
                await cn.OpenAsync();
                da.Fill(ds);
                output.AddRange(from DataRow dr in ds.Tables[0].Rows
                                select new Bank
                                {
                                    Id = Convert.ToInt32(dr["id"]),
                                    Name = Convert.ToString(dr["name"])

                                });

            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                da.Dispose();
                ds.Dispose();
                cmd.Dispose();
                cn.Dispose();
            }
            return output;
        }
        public async Task<int> InsertBank(string name)
        {
            int bank_id = 0;
            var cn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand("sp_InsertBank", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.Add("@bank_id", SqlDbType.Int);
            cmd.Parameters["@bank_id"].Direction = ParameterDirection.Output;
            try
            {
                cn.Open();
                await cmd.ExecuteNonQueryAsync();
                bank_id = Convert.ToInt32(cmd.Parameters["@bank_id"].Value);

            }
            finally
            {
                cn.Close();
                cmd.Dispose();
                cn.Dispose();
            }
            return bank_id;
        }

        public async Task<Bank> GetBank(int id)
        {
            Bank output;
            var cn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand("sp_GetBankById", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@id", id);
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            try
            {
                await cn.OpenAsync();
                da.Fill(ds);
                cn.Close();
                if (ds.Tables[0].Rows.Count == 0)
                    throw new RowNotInTableException("Invalid Id");
                var row = ds.Tables[0].Rows[0];
                output = new Bank
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = Convert.ToString(row["name"])
                };
            }
            finally
            {
                da.Dispose();
                ds.Dispose();
                cmd.Dispose();
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return output;

        }

        public async Task<bool> DeleteBank(int id)
        {
            var cn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand("sp_DeleteBank", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                cn.Open();
                await cmd.ExecuteNonQueryAsync();

            }
            finally
            {
                cn.Close();
                cmd.Dispose();
                cn.Dispose();
            }
            return true;
        }

        public async Task<bool> UpdateBank(int id, string name)
        {
            var cn = new SqlConnection(ConnectionString);
            var cmd = new SqlCommand("sp_UpdateBank", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            try
            {
                cn.Open();
                await cmd.ExecuteNonQueryAsync();

            }
            finally
            {
                cn.Close();
                cmd.Dispose();
                cn.Dispose();
            }
            return true;
        }
    }

}
