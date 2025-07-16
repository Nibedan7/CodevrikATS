using System.Data;
using CdplATS.API.Repositories;
using CdplATS.Entity.Models;
using Microsoft.Data.SqlClient;

namespace CdplATS.API.Services
{
    public class ProjectTrackerService
    {
        private readonly GenericRepository _genericRepository;
        public ProjectTrackerService(GenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<ApiResponse<IEnumerable<ProjectTrackerCellViewModel>>> GetProjectTrackerList(int Status = 1)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Status",Status)
                };
                

                var PTracker = await _genericRepository.GetAllAsync<ProjectTrackerCellViewModel>("GetProjectTracker",parameters);
                return new ApiResponse<IEnumerable<ProjectTrackerCellViewModel>>(PTracker, "Client data retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<ProjectTrackerCellViewModel>>(null, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<ProjectTrackerCellAddEdit>> AddEditColumnRow(ProjectTrackerCellAddEdit request)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                    new SqlParameter("@Type", SqlDbType.Int) { Value = request.Type },
                    new SqlParameter("@Name", SqlDbType.NVarChar) { Value = request.Name },
                };


                var result = await _genericRepository.GetAsync<dynamic>("AddEditPtColumnRow", parameters);

                if (result.StatusCode == -1)
                {
                    return new ApiResponse<ProjectTrackerCellAddEdit> { Success = false, Message = "Column Name already exists!" };
                }
                else
                {
                    if (request.Id == 0)
                    {
                        return new ApiResponse<ProjectTrackerCellAddEdit> { Success = true, Message = "Row/Column added successfully" };
                    }
                    else
                    {
                        return new ApiResponse<ProjectTrackerCellAddEdit> { Success = true, Message = "Row?Column Updated successfully" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProjectTrackerCellAddEdit> { Success = false, Message = $"Error inserting/updating Row/Column data: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<string>> AddEditPtValue(ProjectTrackerCellAddEdit request)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ColumnId", SqlDbType.Int) { Value = request.ColumnId },
                    new SqlParameter("@RowId", SqlDbType.Int) { Value = request.RowId },
                    new SqlParameter("@Value", SqlDbType.NVarChar) { Value = request.Value }
                };


                var result = await _genericRepository.GetAsync<dynamic>("AddEditPtValue", parameters);

                if (result.StatusCode == 1)
                {
                    return new ApiResponse<string> { Success = true, Message = "Value Added/Updated Successfully." };
                }

                return new ApiResponse<string> { Success = true, Message = "Operation completed." };

            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = $"Error inserting/updating Value: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<bool>> DeletePtColumnRow(int Id,int Type,int Status)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", Id),
                    new SqlParameter("@Type", Type),
                    new SqlParameter("@Status", Status)
                };
                var result = await _genericRepository.ExecuteAsync("DeletePtColumnRow", parameters);
                if (result == 0)
                {
                    return new ApiResponse<bool>(true, "Error: No Column/Row data returned.", true);
                }
                return new ApiResponse<bool>(true, "Column/Row deleted successfully.", true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(false, $"Error: {ex.Message}", false);
            }
        }

        public async Task<ApiResponse<string>> ReorderProjectTracker(ProjectTrackerCellAddEdit request)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = request.Id },
                    new SqlParameter("@Type", SqlDbType.Int) { Value = request.Type },
                    new SqlParameter("@Direction", SqlDbType.NVarChar) { Value = request.Direction }
                };


                var result = await _genericRepository.GetAsync<dynamic>("ReorderProjectTracker", parameters);

                if (result.StatusCode == 1)
                {
                    return new ApiResponse<string> { Success = true, Message = "Value Updated Successfully." };
                }

                return new ApiResponse<string> { Success = true, Message = "Operation completed." };

            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = $"Error updating Value: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<string>> UpdatePtColor(ProjectTrackerCellAddEdit request)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TargetId", SqlDbType.Int) { Value = request.TargetId },
                    new SqlParameter("@Type", SqlDbType.Int) { Value = request.Type },
                    new SqlParameter("@ColorCode", SqlDbType.Int) { Value = request.ColorCode }
                };


                var result = await _genericRepository.GetAsync<dynamic>("UpdatePtColor", parameters);

                if (result.StatusCode == 1)
                {
                    return new ApiResponse<string> { Success = true, Message = "color Updated Successfully." };
                }

                return new ApiResponse<string> { Success = true, Message = "Operation completed." };

            }
            catch (Exception ex)
            {
                return new ApiResponse<string> { Success = false, Message = $"Error updating Value: {ex.Message}" };
            }
        }

    }
}
