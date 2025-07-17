using CodeVrikATS.API.Repositories;
using CodeVrikATS.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeVrikATS.API.Services
{
    public class AccountService
    {
        private readonly GenericRepository _genericRepository;

        public AccountService(GenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ApiResponse<LoggerEntity>> CheckUserLogin(LoginEntity entity)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserCode", SqlDbType.Int) { Value = entity.UserCode },
                    new SqlParameter("@Password", SqlDbType.VarChar) { Value = entity.Password}
                };

                var user = await _genericRepository.GetAsync<LoggerEntity>("CheckUserLogin", parameters);
                if (user == null)
                {
                    return new ApiResponse<LoggerEntity>(null, "Invalid EmployeeCode or Password!",false);
                }
                return new ApiResponse<LoggerEntity>(user, "User retrieved successfully.",true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<LoggerEntity>(null, $"Error: {ex.Message}", false);
            }
        }

        // email service
        public async Task<ApiResponse<IEnumerable<EmailEntity>>> GetEmailByEmployeeCode(int EmployeeCode, string EncUserCode)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeCode", EmployeeCode),
                new SqlParameter("@EncUserCode", EncUserCode),
            };

            var result = await _genericRepository.GetAllAsync<EmailEntity>("GetUserEmailByEmployeCode", parameters);

            return new ApiResponse<IEnumerable<EmailEntity>>(result, "Email retrieved successfully.");

        }

    }
}
