using System.Reflection.Emit;
using CodeVrikATS.Entity.Models;
using CodeVrikATS.UI.Helpers;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Interfaces;

namespace CodeVrikATS.UI.Controllers
{
    public class DepartmentController : AuthController
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();

        public DepartmentController(WebApiHelper webApiHelper)
        {
            _webApiHelper = webApiHelper;
        }

        public  IActionResult Index()
        {
            // Prevent caching
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartmentWithEmployees()
        {
            var url = _apiHelper.GetDepartmentWithEmployees;
            var response = await _webApiHelper.GetAsync<List<DepartmentEntity>>(url);
            return Json(response?.Data ?? new List<DepartmentEntity>());
        }


        [HttpGet]
        public async Task<IActionResult> GetDepartmentDetail(int DepartmentId)
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            var employee = await _webApiHelper.GetAsync<List<EmployeeEntity>>(_apiHelper.BindEmployeeList + $"{LoggedEmpCode}");
            DepartmentEntity department = new DepartmentEntity
            {
                EmployeeList = employee.Data
            };

            if (DepartmentId != 0)
            {
                var departments = await _webApiHelper.GetAsync<List<DepartmentEntity>>(_apiHelper.GetDepartmentWithEmployees + $"?DepartmentId={DepartmentId}");

                department = departments.Data[0];
                department.EmployeeList = employee.Data;

            }

            return PartialView("/Views/Department/AddEditDepartment.cshtml", department);
        }

        [HttpGet]

        public async Task<IActionResult> BindEmployeeForDepartment(int LeaderId)
        {
            var employeesList = await _webApiHelper.GetAsync<List<EmployeeEntity>>(_apiHelper.BindEmployeeList + $"{LeaderId}");
;
            return Json(employeesList.Data);
        }
        [HttpPost]
        public async Task<IActionResult> AddEditDepartment([FromBody] DepartmentEntity request)
        {
            var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");
            request.LoggedEmployeeCode = LoggedEmpCode;
            if (request == null)
            {
                return BadRequest("Invalid Data");
            }

            if (ModelState.IsValid)
            {
                var response = await _webApiHelper.PostAsync<dynamic>(_apiHelper.AddEditDepartmentApi, request);

                if (response == null)
                {
                    return StatusCode(500, "Error adding/editing department.");
                }
                return Json(response);
            }
            return BadRequest("Model validation failed.");
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Invalid Department Id");
            }
            var response = await _webApiHelper.DeleteAsync<bool>(_apiHelper.DeleteDepartmentApi + $"{Id}");

            if (response == null || !response.Success)
            {
                return Json(response?.Message);
            }
            return Json(response);
        }

    }
}
