using CodeVrikATS.Entity.Models;
using CodeVrikATS.UI.Helpers;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;

namespace CodeVrikATS.UI.Controllers
{
    public class EmployeeChartController : Controller
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();

        public EmployeeChartController(WebApiHelper webApiHelper)
        {
            _webApiHelper = webApiHelper;
        }

        public async Task<IActionResult> Index()
        {
            // Prevent caching
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var loggedEmpCode = HttpContext.Session.GetInt32("authCode");

            if (loggedEmpCode == null)
                return RedirectToAction("Index", "Account");
            var EmployeeHierarchyList = await _webApiHelper.GetAsync<List<EmpDropDownList>>(_apiHelper.BindEmployeeHierarchy + $"{loggedEmpCode}");
            var Years = await _webApiHelper.GetAsync<List<int>>(_apiHelper.BindYearListApi);

            EmployeeChartEntity EmployeeChartEntity = new EmployeeChartEntity
            {
                EmployeeHierarchyList = EmployeeHierarchyList.Data,
                SelectedEmployeeCode = loggedEmpCode,
                bindYear = Years.Data,
                Year = DateTime.Now.Year
            };

            return View(EmployeeChartEntity);
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployeeWorkHourLineChart(int employeeCode, int year, int month)
        {
            try
            {
                // Convert month=0 to null for the stored procedure
                int? monthParam = month == 0 ? null : month;

                var url = $"{_apiHelper.WorkHourReportApi}?employeeCode={employeeCode}&year={year}";
                if (monthParam.HasValue)
                {
                    url += $"&month={monthParam.Value}";
                }

                var workhourLineChart = await _webApiHelper.GetAsync<List<EmployeeChartEntity>>(url);

                if (workhourLineChart?.Data == null || !workhourLineChart.Data.Any())
                {
                    return NotFound("No data found.");
                }

                return Json(workhourLineChart.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeAverageWorkHourBarChart(int employeeCode, int year)
        {
            try
            {
               
                int? yearParam = year == 0 ? null : year;

                var averageWorkHourData = await _webApiHelper.GetAsync<List<AverageWorkHourEntity>>(_apiHelper.AverageWorkHourReportApi+$"?employeeCode={employeeCode}&year={year}");

                if (averageWorkHourData?.Data == null || !averageWorkHourData.Data.Any())
                {
                    return NotFound("No data found for average work hours.");
                }

                return Json(averageWorkHourData.Data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPunctualityRateLineChart(int employeeCode, int year)
        {
            try
            {

                int? yearParam = year == 0 ? null : year;

                var punctualRateData = await _webApiHelper.GetAsync<List<PunctualityRateEntity>>(_apiHelper.PunctualityRateReportApi + $"?employeeCode={employeeCode}&year={year}");

                if (punctualRateData?.Data == null || !punctualRateData.Data.Any())
                {
                    return NotFound("No data found for punctuality rate.");
                }

                return Json(punctualRateData.Data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetWFHvsOfficeReport(int employeeCode, int year,int month)
        {
            try
            {

                int? yearParam = year == 0 ? null : year;

                var WFHvsWFO = await _webApiHelper.GetAsync<List<GetWFHvsOfficeEntity>>(_apiHelper.GetWFHvsOfficeReportApi + $"?employeeCode={employeeCode}&year={year}&month={month}");

                if (WFHvsWFO?.Data == null || !WFHvsWFO.Data.Any())
                {
                    return NotFound("No data found for punctuality rate.");
                }

                return Json(WFHvsWFO.Data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetMonthlyWorkHour(int employeeCode, int year)
        {
            try
            {

                int? yearParam = year == 0 ? null : year;

                var workhour = await _webApiHelper.GetAsync<List<GetMonthlyWorkHourEntity>>(_apiHelper.GetMonthlyWorkHourApi + $"?employeeCode={employeeCode}&year={year}");

                if (workhour?.Data == null || !workhour.Data.Any())
                {
                    return NotFound("No data found for work hour.");
                }

                return Json(workhour.Data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
