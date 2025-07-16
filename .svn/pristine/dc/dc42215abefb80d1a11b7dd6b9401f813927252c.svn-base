using System.Data;
using CdplATS.API.Repositories;
using CdplATS.Entity.Models;
using Microsoft.Data.SqlClient;

namespace CdplATS.API.Services
{
    public class HolidayService
    {
        private readonly GenericRepository _genericRepository;

        public HolidayService(GenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }


        #region Service for get one holiday details
        public async Task<ApiResponse<HoliDayEntity>> GetHolidayDetails(int Id,int Year )
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                    new SqlParameter("@Year", SqlDbType.Int) { Value = Year }

                };

                var holidays = await _genericRepository.GetAsync<HoliDayEntity>("GetHolidayDetails", parameters);


                return new ApiResponse<HoliDayEntity>(holidays, "Holidays retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<HoliDayEntity>(null, $"Error: {ex.Message}", false);
            }
        }
        #endregion

        #region Service for get all Holiday list
        public async Task<ApiResponse<IEnumerable<HoliDayEntity>>> GetHolidayList(int? Year = null)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
               
                    new SqlParameter("@Year", SqlDbType.Int) { Value = Year }

                };

                var holidays = await _genericRepository.GetAllAsync<HoliDayEntity>("GetHolidayList", parameters);


                return new ApiResponse<IEnumerable<HoliDayEntity>>(holidays, "Holidays retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<HoliDayEntity>>(null, $"Error: {ex.Message}", false);
            }
        }
        #endregion

        #region service for Add and Edit Holiday
        public async Task<ApiResponse<HoliDayEntity>> AddEditHoliday(HoliDayEntity holiday)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", holiday.Id),
                    new SqlParameter("@FestivalName", holiday.FestivalName),
                    new SqlParameter("@FestivalDate", holiday.Date)
                };

                var result = await _genericRepository.GetAsync<dynamic>("AddEditFestival", parameters);

                if (result.StatusCode == -1)
                {
                    return new ApiResponse<HoliDayEntity> { Success = false, Message = "Holiday Date already exists!" };
                }
                else
                {
                    if (holiday.Id == 0)
                    {
                        return new ApiResponse<HoliDayEntity> { Success = true, Message = "Holiday added successfully" };
                    }
                    else
                    {
                        return new ApiResponse<HoliDayEntity> { Success = true, Message = "Holiday Updated successfully" };
                    }
                }


            }
            catch (Exception ex)
            {

                return new ApiResponse<HoliDayEntity> { Success = false, Message = $"Error inserting/updating Holiday data: {ex.Message}" };
            }
        }

        #endregion



        #region Service for Delete Holiday
        public async Task<ApiResponse<bool>> DeleteHoliday(int Id)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", Id)
                };
                var result = await _genericRepository.ExecuteAsync("DeleteFestival", parameters);
                if (result == 0)
                {
                    return new ApiResponse<bool>(true,"Error: No holiday data returned.", true);
                }
                return new ApiResponse<bool>(true, "Holiday deleted successfully.",true);
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>(false, $"Error: {ex.Message}", false);
            }
        }
        #endregion

    }
}
