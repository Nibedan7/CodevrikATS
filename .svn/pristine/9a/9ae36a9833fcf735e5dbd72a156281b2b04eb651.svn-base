using CdplATS.Entity.Models;
using CdplATS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CdplATS.UI.Controllers
{
    public class AppraisalController : AuthController
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();

        public AppraisalController(WebApiHelper webApiHelper)
        {
            this._webApiHelper = webApiHelper;
        }
        public async Task<IActionResult> Index()
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var apprisalEmployeeList = await _webApiHelper.GetAsync<List<BindApprisalEmployeesList>>(_apiHelper.BindApprisalEmployeesListApi + $"{LoggedEmpCode}");
            var defaultAppraislEmployee = apprisalEmployeeList.Data?.FirstOrDefault(e => e.EmployeeCode == LoggedEmpCode) ?? apprisalEmployeeList.Data.FirstOrDefault();
            AppraisalEntity appraisalEntity = new AppraisalEntity
            {
                AppraisalEmployeeList = apprisalEmployeeList.Data,
                DefaultAppraisalEmployee = defaultAppraislEmployee.EmployeeCode
            };
            return View(appraisalEntity);
        }

        public async Task<IActionResult> BindEmployeeAppraisalList(int EmployeeCode)
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var apprisalList = await _webApiHelper.GetAsync<List<BindApprisalByEmployeeList>>(_apiHelper.BindApprisalByEmployeeListApi + $"{EmployeeCode}/{LoggedEmpCode}");
            return Json(apprisalList.Data);
        }

        public async Task<IActionResult> LoadTab1(int EmployeeCode, int AssessmentId)
        {
            var TabId = 1;
            var apprisalList = await _webApiHelper.GetAsync<List<AppraisalEntity>>(_apiHelper.GetApprisalApi + $"{TabId}/{EmployeeCode}/{AssessmentId}");
            return PartialView("/Views/Appraisal/Tab1.cshtml", apprisalList.Data);
        }

        public async Task<IActionResult> LoadTab2(int EmployeeCode,int AssessmentId)
        {
            var TabId = 2;
            var appraisalList = await _webApiHelper.GetAsync<List<AppraisalEntity>>(_apiHelper.GetApprisalApi + $"{TabId}/{EmployeeCode}/{AssessmentId}");
            return PartialView("Views/Appraisal/Tab2.cshtml",appraisalList.Data);
        }


        public async Task<IActionResult> LoadTab3(int EmployeeCode, int AssessmentId)
        {
            var TabId = 3;
            var appraisalList = await _webApiHelper.GetAsync<List<AppraisalEntity>>(_apiHelper.GetApprisalApi + $"{TabId}/{EmployeeCode}/{AssessmentId}");
            return PartialView("Views/Appraisal/Tab3.cshtml", appraisalList.Data);
        }

        public async Task<IActionResult> LoadTab4(int EmployeeCode, int AssessmentId)
        {
            var TabId = 4;
            //var appraisalList = await _webApiHelper.GetAsync<List<AppraisalEntity>>(_apiHelper.GetApprisalApi + $"{TabId}/{EmployeeCode}/{AssessmentId}");
            return PartialView("Views/Appraisal/Tab4.cshtml");
        }

        public async Task<IActionResult> GetTabAvarageByEmployee(int EmployeeCode, int TabId, int EmpAssessementId)
        {
            var avarageTabRating = await _webApiHelper.GetAsync<List<AvarageAppraisal>>(_apiHelper.GetTabAvarageByEmployee + $"{EmployeeCode}/{TabId}/{EmpAssessementId}");
            return Json(avarageTabRating.Data[0]);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAssessment([FromBody] List<AppraisalEntity> appraisalEntities)
        {
            var jsonAssessment = JsonConvert.SerializeObject(appraisalEntities);
            var appraisalModel = new AppraisalEntity
            {
                JsonString = jsonAssessment
            };
            var response = await _webApiHelper.PostAsync<dynamic>(_apiHelper.UpdateApprisalApi, appraisalModel);
            return Json(response);
        }
    }
}
