

using CodeVrikATS.API.Repositories;
using CodeVrikATS.Entity.Models;

namespace CodeVrikATS.API.Services
{
    public class UtilityService
    {
        private readonly GenericRepository _genericRepository;
        public UtilityService(GenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ApiResponse<IEnumerable<EmployeeFileEntity>>> GetEmployeeInfo()
        {
            try
            {
                var Employees = await _genericRepository.GetAllAsync<EmployeeFileEntity>("GetEmployeeInfo");
                return new ApiResponse<IEnumerable<EmployeeFileEntity>>(Employees, "Employee Details retrieved successfully.");
            }
            catch
            {
                return new ApiResponse<IEnumerable<EmployeeFileEntity>>(null, "Error retrieving employee information.", false);
            }
        }

        public async Task<ApiResponse<string>> ReCalculatedPunchData()
        {
            try
            {
                var employee = await _genericRepository.GetAsync<dynamic>("RecalculatePunchData");

                if (employee.TotalRowsAdded <= 0)
                {
                    return new ApiResponse<string>(null, "No punch data found to recalculate.", false);
                }
                else
                {
                    return new ApiResponse<string>( "Punch data recalculated successfully.");
                }

            }
            catch (Exception ex)
            {
                // Log this exception (optional)
                return new ApiResponse<string>(null, $"Exception: {ex.Message}", false);
            }
        }


    }
}
