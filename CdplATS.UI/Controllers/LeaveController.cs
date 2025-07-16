using CdplATS.Entity.Models;
using CdplATS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CdplATS.UI.Controllers
{
    public class LeaveController : AuthController
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();
        private readonly CommonService _commonService;

        public LeaveController(WebApiHelper webApiHelper, CommonService commonService)
        {
            _webApiHelper = webApiHelper;
            _commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var CurrYear = DateTime.Now.Year;

            var EmployeeHierarchy = await _webApiHelper.GetAsync<List<EmpDropDownList>>(_apiHelper.BindEmployeeHierarchy + $"{LoggedEmpCode}");
            var LeaveYears = await _webApiHelper.GetAsync<List<int>>(_apiHelper.BindYearListApi);

            var leaveModel = new LeaveEntity
            {
                EmployeeDropDownList = EmployeeHierarchy.Data,
                LogEmpCode = LoggedEmpCode,
                LeaveYears = LeaveYears.Data,
                CurrentYear = CurrYear,
            };
            return View(leaveModel);
        }

        // Get All leave of a Employee
        [HttpGet]
        public async Task<IActionResult> GetLeaveList(int empCode, int Status, int LeaveYear)
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var leaves = await _webApiHelper.GetAsync<List<LeaveEntity>>(_apiHelper.GetLeaveListByEmpCodeApi + $"{empCode}/{Status}/{LeaveYear}/{LoggedEmpCode}");
            return Json(leaves.Data);
        }

        //Add or Edit Leave
        [HttpGet]
        public async Task<IActionResult> GetLeaveDetails(string GroupId)
        {
            LeaveEntity leaveEntity= new LeaveEntity();
            if (GroupId != "0")
            {
                var leave = await _webApiHelper.GetAsync<List<LeaveEntity>>(_apiHelper.GetLeaveByGroupIdApi + $"{GroupId}");
                leaveEntity = leave.Data[0];
            }

            return PartialView("/Views/Leave/AddEditLeave.cshtml", leaveEntity);

        }

        // Save Leave
        [HttpPost]
        public async Task<IActionResult> SaveLeave(LeaveEntity leave)
        {
            if (ModelState.IsValid)
            {
                var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
                leave.EmpCode = LoggedEmpCode;
                var result = await _webApiHelper.PostAsync<dynamic>(_apiHelper.AddEditLeaveApi, leave);

                if (result.Success)
                {
                     await _webApiHelper.PostAsync<dynamic>(_apiHelper.SendLeaveAppliedEmailApi, leave);

                }   
                    
                return Json(result);
            }
            return View("Index");
        }


        //Delete Leave
        [HttpPost]
        public async Task<IActionResult> DeleteLeave(string groupId)
        {
            var result = await _webApiHelper.DeleteAsync<dynamic>(_apiHelper.DeleteLeaveApi + $"{groupId}");
            return Json(result);
        }

        // Approve or Reject Leave
        [HttpPost]
        public async Task<IActionResult> ApproveRejectLeave(string groupId, int Status)
           {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var result = await _webApiHelper.DeleteAsync<dynamic>(_apiHelper.ApproveRejectLeaveApi + $"{groupId}/{Status}/{LoggedEmpCode}");

            if (result.Success)
            {
                await _webApiHelper.DeleteAsync<dynamic>(_apiHelper.SendLeaveApproveRejectEmailApi + $"{groupId}");

            }

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaveCount(int EmpCode, int Year)
        {
            var leaveCount = await _webApiHelper.GetAsync<List<EmpDropDownList>>(_apiHelper.GetLeaveCountByEmpCodeApi + $"{EmpCode}/{Year}");

            return Json(leaveCount.Data[0]);
        }
    }
}
