using CodeVrikATS.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardApiController : ControllerBase
    {
        private readonly DashboardService _chartService;

        public DashboardApiController(DashboardService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet("GetTotalWorkHourPerEmployee")]
        public async Task<IActionResult> GetTotalWorkHourPerEmployee(int? year = null, int? month = null)
        {
            var response = await _chartService.GetTotalWorkHourPerEmployee(year, month);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }


        [HttpGet("GetMonthlyAvgWorkHoursPerDay")]
        public async Task<IActionResult> GetMonthlyAvgWorkHoursPerDay(int? year = null, int? month = null)
        {
            var response = await _chartService.GetMonthlyAvgWorkHoursPerDay(year, month);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetEmployeePunctuality")]
        public async Task<IActionResult> GetEmployeePunctuality(int? year = null, int? month = null)
        {
            var response = await _chartService.GetEmployeePunctuality(year, month);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetAnnualLeaveSummaryByYear")]
        public async Task<IActionResult> GetAnnualLeaveSummaryByYear(int? year = null)
        {
            var response = await _chartService.GetAnnualLeaveSummaryByYear(year);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("MonitorHybridWorkBalance")]
        public async Task<IActionResult> MonitorHybridWorkBalance(int? year = null, int? month = null)
        {
            var response = await _chartService.MonitorHybridWorkBalance(year, month);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
