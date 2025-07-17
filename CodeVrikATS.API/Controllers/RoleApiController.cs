using CodeVrikATS.API.Services;
using CodeVrikATS.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleApiController : ControllerBase
    {
        private readonly RoleServices _roleServices;

        public RoleApiController(RoleServices roleServices)
        {
            this._roleServices = roleServices;
        }

        /// <summary>
        /// Get Roles
        /// </summary>
        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int RoleId = 0)
        {
            var response = await _roleServices.GetRoleById(RoleId);
            return Ok(response);
        }

        /// <summary>
        /// Add / Edit Roles
        /// </summary>  
        ///

        [HttpPost("addEditRole")]
        public async Task<IActionResult> AddEditRole([FromBody] RoleEntity roleEntity)
        {
            var response = await _roleServices.AddEditRoles(roleEntity);
            return Ok(response);
        }

        [HttpDelete("deleteRole/{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            var response = await _roleServices.DeleteRole(roleId);
            return Ok(response);
        }
    }
}
