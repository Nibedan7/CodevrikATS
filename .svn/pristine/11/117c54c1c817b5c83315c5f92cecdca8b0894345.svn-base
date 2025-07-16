using CdplATS.Entity.Models;
using CdplATS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CdplATS.UI.Controllers
{
    public class LeaveReportController : AuthController
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();

        public LeaveReportController(WebApiHelper webApiHelper)
        {
            this._webApiHelper = webApiHelper;
        }
        public async Task<IActionResult> Index()
        {
            var Years = await _webApiHelper.GetAsync<List<int>>(_apiHelper.BindYearListApi);

            LeaveReportEntity leaveReportModel = new LeaveReportEntity
            {
                Years = Years.Data,
            };
            return View(leaveReportModel);
        }

        public async Task<IActionResult> GetLeaveReportList(string? StartDate = null, string? EndDate = null)
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var response = await _webApiHelper.GetAsync<List<LeaveReportEntity>>(_apiHelper.GetLeaveReporListApi + $"?EmpCode={LoggedEmpCode}&StartDate={StartDate}&EndDate={EndDate}");
            return Json(response.Data);
        }

    }
}
