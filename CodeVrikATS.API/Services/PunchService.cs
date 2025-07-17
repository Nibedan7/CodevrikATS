using CodeVrikATS.API.Repositories;
using CodeVrikATS.Entity.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeVrikATS.API.Services
{
    public class PunchService
    {
        private readonly GenericRepository _genericRepository;
       
        public PunchService(GenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<ApiResponse<IEnumerable<PunchEntity>>> GetPunchByEmpCode(int EmpCode, DateTime? StartDate, DateTime? EndDate)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmpCode", SqlDbType.Int) { Value = EmpCode },
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = (object?)StartDate ?? DBNull.Value },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = (object?)EndDate ?? DBNull.Value }
                };

                var punches = await _genericRepository.GetAllAsync<PunchEntity>("GetPunchByEmpCode", parameters);
                return new ApiResponse<IEnumerable<PunchEntity>>(punches, "Punch retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<PunchEntity>>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<IEnumerable<PunchEntity>>> GetEmployeesWorkingTime(int EmpCode, DateTime? StartDate, DateTime? EndDate)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmpCode", SqlDbType.Int) { Value = EmpCode },
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = (object?)StartDate ?? DBNull.Value },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = (object?)EndDate ?? DBNull.Value }
                };

                var punches = await _genericRepository.GetAllAsync<PunchEntity>("GetEmployeesWorkingTime", parameters);
                return new ApiResponse<IEnumerable<PunchEntity>>(punches, "data retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<PunchEntity>>(null, $"Error: {ex.Message}", false);
            }
        }


    }
}
