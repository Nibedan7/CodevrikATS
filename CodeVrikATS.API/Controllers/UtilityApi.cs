using CodeVrikATS.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityApi : ControllerBase
    {
        public readonly UtilityService _utilityService;
        public UtilityApi(UtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        [HttpGet("GetEmployeeInfo")]
        public async Task<IActionResult> GetEmployeeFileList()
        {
            var response = await _utilityService.GetEmployeeInfo();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("ReCalculatePunchData")]
        public async Task<IActionResult> ReCalculatePunchData()
        {
            var response = await _utilityService.ReCalculatedPunchData();

            return Ok(response);
        }


    }
}
