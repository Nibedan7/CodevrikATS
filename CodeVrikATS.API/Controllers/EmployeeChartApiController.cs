using CodeVrikATS.API.Services;
using CodeVrikATS.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeChartApiController : ControllerBase
    {
        private readonly EmployeeChartService _reportService;

        public EmployeeChartApiController(EmployeeChartService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Get Employee Work Hours Report (Daily or Monthly)
        /// </summary>

        [HttpGet("GetWorkHoursReport")]
        public async Task<IActionResult> GetEmployeeWorkHoursReport([FromQuery] int EmployeeCode, [FromQuery] int? Month = null, [FromQuery] int? Year = null)
        {
            var response = await _reportService.GetEmployeeWorkHoursReport(EmployeeCode, Month, Year);
            return Ok(response);
        }

        [HttpGet("GetAverageWorkHoursReport")]
        public async Task<IActionResult> GetEmployeeAverageWorkHour(int EmployeeCode, int? Year = null)
        {
            var response = await _reportService.GetEmployeeAverageWorkHour(EmployeeCode, Year);
            return Ok(response);
        }

        [HttpGet("GetEmployeePunctualityRateReport")]
        public async Task<IActionResult> GetEmployeePunctualityRateReport(int EmployeeCode, int? Year = null)
        {
            var response = await _reportService.GetEmployeePunctualityRate(EmployeeCode, Year);
            return Ok(response);
        }

        [HttpGet("GetWFHvsOfficeReport")]
        public async Task<IActionResult> GetWFHvsOfficeReport(int EmployeeCode, int Month, int? Year = null)
        {
            var response = await _reportService.GetWFHvsOfficeReport(EmployeeCode,Month, Year);
            return Ok(response);
        }

        [HttpGet("GetMonthlyWorkHour")]
        public async Task<IActionResult> GetMonthlyWorkHour(int EmployeeCode, int? Year = null)
        {
            var response = await _reportService.GetMonthlyWorkHour(EmployeeCode, Year);
            return Ok(response);
        }
    }
}
