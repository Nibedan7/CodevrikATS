using CdplATS.API.Repositories;
using CdplATS.Entity.Models;
using Microsoft.Data.SqlClient;
using System.Data;

public class EmployeeChartService
{
    private readonly GenericRepository _genericRepository;

    public EmployeeChartService(GenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<ApiResponse<IEnumerable<EmployeeChartEntity>>> GetEmployeeWorkHoursReport(int EmployeeCode, int? Month, int? Year)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                new SqlParameter("@Month", SqlDbType.Int) { Value = Month },
                new SqlParameter("@Year", SqlDbType.Int) { Value = Year }
            };

            var reports = await _genericRepository.GetAllAsync<EmployeeChartEntity>("GetEmployeeWorkHoursReport", parameters);
            return new ApiResponse<IEnumerable<EmployeeChartEntity>>(reports, "Report retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<EmployeeChartEntity>>(null, $"Error: {ex.Message}", false);
        }
    }
    public async Task<ApiResponse<IEnumerable<AverageWorkHourEntity>>> GetEmployeeAverageWorkHour(int EmployeeCode, int? Year)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                new SqlParameter("@Year", SqlDbType.Int) { Value = Year }
            };

            var reports = await _genericRepository.GetAllAsync<AverageWorkHourEntity>("GetEmployeeAverageWorkHour", parameters);
            return new ApiResponse<IEnumerable<AverageWorkHourEntity>>(reports, "Report retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<AverageWorkHourEntity>>(null, $"Error: {ex.Message}", false);
        }
    }

    
    public async Task<ApiResponse<IEnumerable<PunctualityRateEntity>>> GetEmployeePunctualityRate(int EmployeeCode, int? Year)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                new SqlParameter("@Year", SqlDbType.Int) { Value = Year }
            };

            var reports = await _genericRepository.GetAllAsync<PunctualityRateEntity>("GetEmployeePunctualityRate", parameters);
            return new ApiResponse<IEnumerable<PunctualityRateEntity>>(reports, "Report retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<PunctualityRateEntity>>(null, $"Error: {ex.Message}", false);
        }
    }

    public async Task<ApiResponse<IEnumerable<GetWFHvsOfficeEntity>>> GetWFHvsOfficeReport(int EmployeeCode, int Month, int? Year)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                new SqlParameter("@Year", SqlDbType.Int) { Value = Year },
                new SqlParameter("@month",SqlDbType.Int){ Value = Month}
            };

            var reports = await _genericRepository.GetAllAsync<GetWFHvsOfficeEntity>("GetWFHvsOfficeReport", parameters);
            return new ApiResponse<IEnumerable<GetWFHvsOfficeEntity>>(reports, "Report retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<GetWFHvsOfficeEntity>>(null, $"Error: {ex.Message}", false);
        }
    }


    public async Task<ApiResponse<IEnumerable<GetMonthlyWorkHourEntity>>> GetMonthlyWorkHour(int EmployeeCode, int? Year)
    {
        try
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                new SqlParameter("@Year", SqlDbType.Int) { Value = Year },
        
            };

            var reports = await _genericRepository.GetAllAsync<GetMonthlyWorkHourEntity>("GetMonthlyWorkHour", parameters);
            return new ApiResponse<IEnumerable<GetMonthlyWorkHourEntity>>(reports, "Report retrieved successfully.");
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<GetMonthlyWorkHourEntity>>(null, $"Error: {ex.Message}", false);
        }
    }

}