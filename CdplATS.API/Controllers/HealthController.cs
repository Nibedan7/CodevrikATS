using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CdplATS.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("✔️ API is running.");
        }
    }
}
