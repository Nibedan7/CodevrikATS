using CodeVrikATS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveReportApiController : ControllerBase
    {
        private readonly LeaveReportService _leaveReportService;

        public LeaveReportApiController(LeaveReportService leaveReportService)
        {
            this._leaveReportService = leaveReportService;
        }

        /// <summary>
        /// Get Leave Report by Department
        /// </summary>

        [HttpGet("getLeaveReportList")]
        public async Task<IActionResult> GetLeaveReportList([FromQuery] int EmpCode, [FromQuery] string? StartDate, [FromQuery] string? EndDate)
        {
            var response = await _leaveReportService.GetLeaveReportList(EmpCode, StartDate, EndDate);
            return Ok(response);
        }
    }
}
