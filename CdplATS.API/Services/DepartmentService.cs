using CodeVrikATS.API.Repositories;
using CodeVrikATS.Entity.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Threading.Tasks;
using static CodeVrikATS.Entity.Models.DepartmentEntity;
namespace CodeVrikATS.API.Services
{
    public class DepartmentService
    {
        private readonly GenericRepository _genericRepository;
        public DepartmentService(GenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ApiResponse<IEnumerable<DepartmentEntity>>> GetDepartmentWithEmployees(int? DepartmentId)
        {
            try
            {
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int){Value = DepartmentId}
                };
                var departments = await _genericRepository.GetAllAsync<DepartmentEntity>("GetDepartmentWithEmployees", parameter);
                return new ApiResponse<IEnumerable<DepartmentEntity>>(departments, "Department retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<DepartmentEntity>>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<IEnumerable<EmployeeEntity>>> BindEmployeeList(int LoggerId)
        {
            try
            {
                var parameter = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeCode", SqlDbType.Int){Value = LoggerId}
                };
                var empList = await _genericRepository.GetAllAsync<EmployeeEntity>("BindEmployeeList",parameter);
                return new ApiResponse<IEnumerable<EmployeeEntity>>(empList, "Employee retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<EmployeeEntity>>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<string>> AddEditDepartment(DepartmentEntity request)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                     new SqlParameter("@DepartmentId", SqlDbType.Int) { Value = request.DepartmentId },
                     new SqlParameter("@EmpCodes", SqlDbType.Text) { Value = request.SelectEmployees},
                     new SqlParameter("@LeadBy", SqlDbType.Int) { Value = request.LeadBy },
                     new SqlParameter("@DepartmentName", SqlDbType.Text) { Value = request.DepartmentName },
                     new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = request.LoggedEmployeeCode }

                };

                var result = await _genericRepository.GetAsync<dynamic>("AddEditDepartment", parameters);

                if (result.IsSuccess == 1 && request.DepartmentId == 0)
                {
                    return new ApiResponse<string> { Success = true, Message = "Department created successfully." };
                }
                else if (result.IsSuccess == 1 && request.DepartmentId != 0)
                {
                    return new ApiResponse<string> { Success = true, Message = "Department updated successfully." };
                }
                else
                {
                    return new ApiResponse<string> { Success = false, Message = "Failed to insert/update department data." };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = $"Error inserting/updating department: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<bool>> DeleteDepartment(int DepartmentId)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@DepartmentId", DepartmentId)

                };

                var result = await _genericRepository.ExecuteAsync("DeleteDepartment", parameters);

                return new ApiResponse<bool>(true, "Department deleted successfully.", true);

            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(false, $"Error: {ex.Message}", false);
            }
        }
    }

    
}
