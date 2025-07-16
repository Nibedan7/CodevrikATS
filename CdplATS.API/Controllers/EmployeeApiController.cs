using CdplATS.API.Services;
using CdplATS.Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace CdplATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeApiController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeApiController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get EmployeeList 
        /// </summary>
        [HttpGet("GetEmployeeList")]
        public async Task<IActionResult> GetEmployeeList(int? ActiveStatus = null, int Id = 0, int? DepartmentId = null)
        {
            var response = await _employeeService.GetEmployeeList(Id, DepartmentId, ActiveStatus);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }


        /// <summary>
        /// Get one employee details
        /// </summary>
        [HttpGet("GetEmployeeDetails")]
        public async Task<IActionResult> GetEmployeeDetails(int? ActiveStatus = null, int Id = 0, int? DepartmentId = null)
        {
            var response = await _employeeService.GetEmployeeDetails(Id, DepartmentId, ActiveStatus);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        //add edit employee controller method
        [HttpPost("AddEditEmployee")]
        public async Task<IActionResult> AddEditEmployee([FromBody] EmployeeEntity employee)
        {
            var response = await _employeeService.AddEditEmployee(employee);
            return Ok(response);
        }
        
        //delete employee controller method
        [HttpDelete("DeleteEmployee/{Id}/{status}")]
        public async Task<IActionResult> DeleteEmployee(int Id,int status)
        {
            var response = await _employeeService.DeleteEmployee(Id,status);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetEmpAssByEmpCode/{Empcode}")]
        public async Task<IActionResult> GetEmpAssByEmpCode(int Empcode)
        {
            var response = await _employeeService.GetEmpAssByEmpCodeAsync(Empcode);

            return Ok(response);
        }

        [HttpPost("StartEmployeeAssessment")]
        public async Task<IActionResult> StartEmployeeAssessment([FromBody] EmployeeAssessmentEntity employeeAssessment)
        {
            var response = await _employeeService.StartEmployeeAssessment(employeeAssessment);
            return Ok(response);
        }
        [HttpPost("SaveEmployeeAssessment")]
        public async Task<IActionResult> SaveEmployeeAssessment([FromBody] EmployeeAssessmentEntity employeeAssessment)
        {
            var response = await _employeeService.SaveEmployeeAssessment(employeeAssessment);
            return Ok(response);
        }
        [HttpPost("AppraisalMail")]
        public async Task<IActionResult> AppraisalMail([FromBody] EmployeeAssessmentEntity employeeAssessment)
        {
            var response = await _employeeService.AppraisalMail(employeeAssessment);
            return Ok(response);
        }

        [HttpPost("AppraisalMailToReviewer2")]
        public async Task<IActionResult> AppraisalMailToReviewer2([FromBody] EmployeeAssessmentEntity employeeAssessment)
        {
            var response = await _employeeService.AppraisalMailToReviewer2(employeeAssessment);
            return Ok(response);
        }
    }
}
