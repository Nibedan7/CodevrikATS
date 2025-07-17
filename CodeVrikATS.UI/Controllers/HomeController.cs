using CodeVrikATS.Entity.Models;
using CodeVrikATS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.UI.Controllers;

public class HomeController : Controller
{
    private readonly WebApiHelper _webApiHelper;

    public HomeController(WebApiHelper webApiHelper)
    {
        _webApiHelper = webApiHelper;
    }


    public async Task<IActionResult> Index()
    {
            return View();
    }
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _webApiHelper.GetAsync<List<LoggerEntity>>("api/userapi/get-all");
        return Json(users.Data);
    }
}
