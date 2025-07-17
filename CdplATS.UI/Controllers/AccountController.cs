using System.Reflection.Emit;
using System.Text;
using CodeVrikATS.Entity.Models;
using CodeVrikATS.UI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodeVrikATS.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CommonService _common;

        public AccountController(WebApiHelper webApiHelper, IHttpContextAccessor httpContextAccessor, CommonService common)
        {
            _webApiHelper = webApiHelper;
            _httpContextAccessor = httpContextAccessor;
            _common = common;
        }
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginEntity model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _webApiHelper.PostUnAuthorizedAsync<dynamic>(_apiHelper.LoginApi, model);

            


            if (response.Success)
            {

                var roleRightsJson = response.Data.RoleRights.ToString();

                var roleRights = JsonConvert.DeserializeObject<List<RoleRightCheck>>(roleRightsJson);

                bool hasDashboardViewRight = roleRights != null &&
                    roleRights.Any(r => r.MenuName == "Dashboard" && r.CanView == 1);


                if (hasDashboardViewRight)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "EmployeeChart");
                }
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
            }
            return View();

        }
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();

            return RedirectToAction("Index", "Account");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromForm] string EncodedUserCode)
        {
            string EncUserCode = EncodedUserCode;


            string decodedUsercode = Encoding.UTF8.GetString(Convert.FromBase64String(EncodedUserCode));
            int employeeCode = int.Parse(decodedUsercode);

            var apiResponse = await _webApiHelper.GetAsync<List<EmailEntity>>(_apiHelper.SendEmailApi + $"?EmployeeCode={employeeCode}&EncUserCode={EncUserCode}");

            var emailEntity = new EmailEntity
            {

                Email = apiResponse.Data[0].Email,
                Subject = apiResponse.Data[0].Subject,
                Body = apiResponse.Data[0].Body
            };

            bool emailResult = await _common.SendEmail(emailEntity);

            if (emailResult)
                return Json(new { success = true, message = "Email sent successfully." });
            else
                return Json(new { success = false, message = "Failed to send email." });
        }



        [HttpGet]
        public IActionResult ChangePassword()
        {
            return PartialView("/Views/Account/ChangePassword.cshtml");
        }
 
        [HttpPost]
        public async Task<JsonResult> ChangePassword(ResetPasswordEntity model)
        {
            var sessionPassword = HttpContext.Session.GetString("Password");

            if (model.CurrentPassword != sessionPassword)
            {
                return Json(new
                {
                    success = false,
                    field = "CurrentPassword",
                    message = "The current password you entered is incorrect."
                });
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return Json(new
                {
                    success = false,
                    field = "ConfirmPassword",
                    message = "Confirm Password doesn't match with New Password."
                });
            }

            var apiResponse = await _webApiHelper.PostAsync<string>(_apiHelper.ResetPasswordApi, model);

            if (apiResponse.Success)
            {
                return Json(new { success = true, message = apiResponse.Data });
            }

            return Json(new
            {
                success = false,
                field = "",
                message = "Password update failed. Try again!"
            });
        }

        public IActionResult AccessDenied()
        {
            return View(); // Create an appropriate AccessDenied.cshtml in Views/Account/
        }

    }
}
