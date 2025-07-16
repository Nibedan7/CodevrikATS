using CdplATS.API.Services;
using CdplATS.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CdplATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AccountService _accountService;

        public AccountApiController(IConfiguration configuration,AccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoggerEntity>>> Login(LoginEntity loginRequest)
        {
            ApiResponse<LoggerEntity> response = await _accountService.CheckUserLogin(loginRequest);

            if (response.Success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]); // Use a strong secret key

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, Convert.ToString(loginRequest.UserCode)) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"], 
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                
                response.Data.UserToken = tokenString;

                return Ok(response);
            }

            return Unauthorized(new ApiResponse<LoggerEntity>
            {
                Success = false,
                Message = response.Message,
                Data = null
            });
        }
        [HttpGet("GetEmailByEmployeeCode")]
        public async Task<IActionResult> GetEmailByEmployeeCode(int EmployeeCode, string EncUserCode)
        {
            var response = await _accountService.GetEmailByEmployeeCode(EmployeeCode, EncUserCode);
            return Ok(response);
        }

    }
}

