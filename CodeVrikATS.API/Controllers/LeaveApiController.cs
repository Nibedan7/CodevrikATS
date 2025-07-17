using CodeVrikATS.API.Services;
using CodeVrikATS.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveApiController : ControllerBase
    {
        private readonly LeaveService _leaveService;

        public LeaveApiController(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        /// <summary>
        /// Get all Leave of a User
        /// </summary>
        [HttpGet("GetLeaveListByEmpCode/{EmpCode}/{Status}/{Year}/{LoggedEmpCode}")]
        public async Task<IActionResult> GetLeaveListByEmpCode(int EmpCode, int Status, int Year, int LoggedEmpCode)
        {
            var response = await _leaveService.GetLeaveByEmpCode(EmpCode, Status, null, Year, LoggedEmpCode);
            return Ok(response);
        }

        /// <summary>
        /// Get Leave by LeaveId
        /// </summary>
        /// 
        [HttpGet("getLeaveByGroupId/{Id}/")]
        public async Task<IActionResult> GetLeaveDetailsByGroupId(string Id)
        {
            var response = await _leaveService.GetLeaveByEmpCode(null, null, Id, null, null);
            return Ok(response);
        }

        /// <summary>
        /// Add / Edit Leave
        /// </summary>  
        ///

        [HttpPost("addEditLeave")]
        public async Task<IActionResult> AddEditLeave([FromBody] LeaveEntity leaveEntity)
        {
            var response = await _leaveService.AddEditLeave(leaveEntity);
            return Ok(response);
        }

        /// <summary>
        /// delete Leave
        /// </summary>  
        ///

        [HttpDelete("deleteLeave/{groupId}")]
        public async Task<IActionResult> DeleteLeave(string groupId)
        {
            var response = await _leaveService.DeleteLeave(groupId);
            return Ok(response);
        }


        /// <summary>
        /// approve&Reject Leave
        /// </summary>  
        ///

        [HttpDelete("approveRejectLeave/{groupId}/{status}/{empCode}")]
        public async Task<IActionResult> ApproveRejectLeave(string groupId, int status, int empCode)
        {
            var response = await _leaveService.ApproveRejectLeave(groupId, status, empCode);
            return Ok(response);
        }


        [HttpGet("GetLeaveCountByEmpCode/{empCode}/{year}")]
        public async Task<IActionResult> GetLeaveCountByEmpCode(int empCode, int year)
        {
            var response = await _leaveService.GetLeaveCountByEmpCode(empCode, year);
            return Ok(response);
        }

        [HttpPost("SendLeaveAppliedEmail")]
        public async Task<IActionResult> SendLeaveAppliedEmail([FromBody] LeaveEntity leaveEntity)
        {
            var response = await _leaveService.SendLeaveAppliedEmail(leaveEntity);
            return Ok(response);
        }

        [HttpDelete("SendLeaveApproveRejectEmail/{GroupId}")]
        public async Task<IActionResult> SendLeaveApproveRejectEmail(string GroupId)
        {
            var response = await _leaveService.SendLeaveApproveRejectEmail(GroupId);
            return Ok(response);
        }
    }
}
