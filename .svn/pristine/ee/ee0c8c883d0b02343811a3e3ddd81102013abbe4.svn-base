using CdplATS.Entity.Models;
using CdplATS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CdplATS.UI.Controllers
{
    public class ManualLogController : AuthController
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();

        public ManualLogController(WebApiHelper webApiHelper)
        {
            this._webApiHelper = webApiHelper;
        }
        public async Task<IActionResult> Index()
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var EmployeeHierarchyList = await _webApiHelper.GetAsync<List<EmpDropDownList>>(_apiHelper.BindEmployeeHierarchy + $"{LoggedEmpCode}");

            var manualLogModel = new ManualLogEntity
            {
                EmployeeDropDownList = EmployeeHierarchyList.Data,
                LogEmpCode = LoggedEmpCode,
            };

            return View(manualLogModel);
        }

        // Get All manual logs of Employee
        [HttpGet]
        public async Task<IActionResult> GetManualLogList(int empCode, int Status)
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var ManualLogs = await _webApiHelper.GetAsync<List<ManualLogEntity>>(_apiHelper.GetManualLogByEmpCodeApi + $"{empCode}/{Status}/{LoggedEmpCode}");
            return Json(ManualLogs.Data);
        }

        //Add or Edit Manual Log
        [HttpGet]
        public async Task<IActionResult> GetManualLogDetails(int ManualLogId)
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            if (ManualLogId != 0)
            {
                var manualLog = await _webApiHelper.GetAsync<List<ManualLogEntity>>(_apiHelper.GetManualLogByManualLogIdApi + $"{ManualLogId}/{LoggedEmpCode}");
                return PartialView("/Views/ManualLog/AddEditManualLog.cshtml", manualLog.Data[0]);
            }

            return PartialView("/Views/ManualLog/AddEditManualLog.cshtml", new ManualLogEntity());
        }

        // Save Manual Log
        [HttpPost]
        public async Task<IActionResult> SaveManualLog( ManualLogEntity manualLog)
        {
            if (ModelState.IsValid)
            {
                var LoggedempCode = HttpContext.Session.GetInt32("authCode");
                manualLog.EmpCode = LoggedempCode.Value;
                var result = await _webApiHelper.PostAsync<dynamic>(_apiHelper.AddEditManualLogApi, manualLog);
                return Json(result);
            }
            return View("Index");
        }

        //Delete Manual Log
        [HttpPost]
        public async Task<IActionResult> DeleteManualLog(int ManualLogId)
        {
            var result = await _webApiHelper.DeleteAsync<dynamic>(_apiHelper.DeleteManualLogApi + $"{ManualLogId}");
            return Json(result);
        }

        // Approve or Reject Manual Log
        [HttpPost]
        public async Task<IActionResult> ApproveRejectManualLog(int ManualLogId, int Status)
        {
            var empCode = HttpContext.Session.GetInt32("authCode");
            var result = await _webApiHelper.DeleteAsync<dynamic>(_apiHelper.ApproveRejectManualLogApi + $"{ManualLogId}/{Status}/{empCode}");
            return Json(result);
        }
    }
}
