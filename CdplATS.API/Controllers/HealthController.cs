using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CdplATS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("✔️ API is running.");
        }
    }
}
