using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.UI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
