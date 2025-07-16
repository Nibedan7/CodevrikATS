using System.Data;
using CdplATS.API.Repositories;
using CdplATS.Entity.Models;
using Microsoft.Data.SqlClient;
using static CdplATS.API.Services.AppraisalService;

namespace CdplATS.API.Services
{
    public class AppraisalService
    {
        private readonly GenericRepository _genericRepository;

        public AppraisalService(GenericRepository genericRepository)
        {
            this._genericRepository = genericRepository;
        }

        public async Task<ApiResponse<IEnumerable<AppraisalEntity>>> GetaAppraisalAsync(int TabId, int EmployeeCode, int AssessmentId)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TabId", SqlDbType.Int) { Value = TabId },
                    new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                    new SqlParameter("@AssessmentId", SqlDbType.Int) { Value = AssessmentId },
                };

                var Appraisals = await _genericRepository.GetAllAsync<AppraisalEntity>("GetAppraisalList", parameters);
                return new ApiResponse<IEnumerable<AppraisalEntity>>(Appraisals, "Appraisal retrieved successfully.");
            }
            catch (Exception ex) 
            {
                return new ApiResponse<IEnumerable<AppraisalEntity>>(null, $"Error: {ex.Message}", false);
            }
        }
        public async Task<ApiResponse<IEnumerable<BindApprisalEmployeesList>>> BindApprisalEmployeesList(int EmployeeCode)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode }
                };

                var Appraisals = await _genericRepository.GetAllAsync<BindApprisalEmployeesList>("BindApprisalEmployeesList", parameters);
                return new ApiResponse<IEnumerable<BindApprisalEmployeesList>>(Appraisals, "Employee retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<BindApprisalEmployeesList>>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<IEnumerable<BindApprisalByEmployeeList>>> BindApprisalByEmployeeList(int EmployeeCode, int LoggedEmpCode)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                    new SqlParameter("@LoggedEmpCode", SqlDbType.Int) { Value = LoggedEmpCode },
                };

                var Appraisals = await _genericRepository.GetAllAsync<BindApprisalByEmployeeList>("BindApprisalByEmployeeList", parameters);
                return new ApiResponse<IEnumerable<BindApprisalByEmployeeList>>(Appraisals, "Employee retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<BindApprisalByEmployeeList>>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<IEnumerable<AvarageAppraisal>>> GetTabAvarageByEmployee(int EmployeeCode, int TabId, int EmpAssessementId)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeCode", SqlDbType.Int) { Value = EmployeeCode },
                    new SqlParameter("@TabId", SqlDbType.Int) { Value = TabId },
                    new SqlParameter("@EmployeeAssessmentId", SqlDbType.Int) { Value = EmpAssessementId },
                };

                var Appraisals = await _genericRepository.GetAllAsync<AvarageAppraisal>("GetTabAverageByEmployeecode", parameters);
                return new ApiResponse<IEnumerable<AvarageAppraisal>>(Appraisals, "Employee retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<AvarageAppraisal>>(null, $"Error: {ex.Message}", false);
            }
        }


        public async Task<ApiResponse<string>> UpdateAssessment(AppraisalEntity appraisalEntity)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@JsonData", SqlDbType.Text) { Value = appraisalEntity.JsonString},
                };

                var result = await _genericRepository.GetAsync<dynamic>("CheckAppraisalUserRole", parameters);

                if (result.IsSuccess == -1)
                {
                    return new ApiResponse<string> { Success = false, Message = "Please fill all the records!" };
                }
                else if (result.IsSuccess == 1)
                {
                    return new ApiResponse<string> { Success = true, Message = "Appraisal Saved Successfully!" };
                }
                else if (result.IsSuccess == 2)
                {
                    return new ApiResponse<string> { Success = true, Message = "Appraisal Submitted Successfully!" };
                }
                else
                {
                    return new ApiResponse<string> { Success = false, Message = "Failed to Update Appraisal data." };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = $"Error While Updating appraisal data: {ex.Message}" };
            }
        }
    }

}

