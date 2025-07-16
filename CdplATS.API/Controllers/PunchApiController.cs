using CdplATS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CdplATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PunchApiController : ControllerBase
    {
        private readonly PunchService _punchService;

        public PunchApiController(PunchService punchService)
        {
            _punchService = punchService;
        }

        /// <summary>
        /// Get Punch Records by Employee Code
        /// </summary>
  
        [HttpGet("GetPunchByEmpCode")]
        public async Task<IActionResult> GetPunchByEmpCode([FromQuery] int EmpCode, [FromQuery] DateTime? StartDate, [FromQuery] DateTime? EndDate)
        {
            var response = await _punchService.GetPunchByEmpCode(EmpCode, StartDate, EndDate);
            return Ok(response);
        }


        [HttpGet("GetEmployeesWorkingTime")]
        public async Task<IActionResult> GetEmployeesWorkingTime([FromQuery] int EmpCode, [FromQuery] DateTime? StartDate, [FromQuery] DateTime? EndDate)
        {
            var response = await _punchService.GetEmployeesWorkingTime(EmpCode, StartDate, EndDate);
            return Ok(response);
        }
    }
}
