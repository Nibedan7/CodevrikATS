using CdplATS.API.Repositories;
using CdplATS.Entity.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CdplATS.API.Services
{
    public class DailyJobService
    {
        private readonly GenericRepository _genericRepository;
        private readonly CommonServices _commonServices;
        private readonly SendEmailCommonAPIService _sendEmailCommonAPIService;

        public DailyJobService(GenericRepository genericRepository,CommonServices commonServices, SendEmailCommonAPIService sendEmailCommonAPIService)
        {
            _genericRepository = genericRepository;
            _commonServices = commonServices;
            _sendEmailCommonAPIService = sendEmailCommonAPIService;
        }

        public async Task<ApiResponse<string>> SetupAllPunchFromEasyTimeToCDPLATS()
        {
            try
            {
                
                var result = await _genericRepository.ExecuteAsync("SetupAllPunchFromEasyTimeToCDPLATS", null);
                return new ApiResponse<string>("Punch setup sccessfully.");                
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>($"Error in setup punch data: {ex.Message}");
            }
        }
        public async Task<ApiResponse<string>> SendLeaveSummaryEmail()
        {
            try
            {
                var result = await _genericRepository.GetAsync<EmailEntity>("SendLeaveSummaryEmail", null);
                // Check if anyone is on leave
                if (result != null && result.Count > 0)
                {
                    await _sendEmailCommonAPIService.SendEmail(result);
                    return new ApiResponse<string>("Leave summary email sent successfully.");
                }
                else
                {
                    return new ApiResponse<string>("No employees on leave today. Email not sent.");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>($"Error in setup punch data: {ex.Message}");
            }
        }

    }
}
