using CodeVrikATS.Entity.Models;
using CodeVrikATS.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodeVrikATS.UI.Controllers
{
    public class RoleRightsManagement : AuthController
    {
        private readonly WebApiHelper _webApiHelper;
        private readonly ApiHelper _apiHelper = new ApiHelper();
        public RoleRightsManagement(WebApiHelper webApiHelper)
        {
            _webApiHelper = webApiHelper;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _webApiHelper.GetAsync<List<RoleEntity>>(_apiHelper.GetRoleById);
            ViewBag.RoleList = roles.Data;
            RoleRightEntity roleRightEntity = new RoleRightEntity
            {
                RolesList = roles.Data,
                SelectedRole = roles.Data[0].RoleId
            };

            return View(roleRightEntity);
        }


        [HttpGet]
        public async Task<IActionResult> GetRoleRightsList(int RoleId)
        {
            var roleRights = await _webApiHelper.GetAsync<List<RoleRightEntity>>(_apiHelper.GetRoleRightListApi + $"{RoleId}");
            return Json(roleRights.Data);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateRoleRight([FromBody]  List<RoleRightEntity> roleRightEntities)
        {
			var jsonString = JsonConvert.SerializeObject(roleRightEntities);
			var LoggedEmpCode = HttpContext.Session.GetInt32("authCode");

			var roleRightsModel = new RoleRightEntity
            {
				RoleRightsJson = jsonString,
                loggedEmpCode = LoggedEmpCode
			};

			var response = await _webApiHelper.PostAsync<dynamic>(_apiHelper.UpdateRoleRightApi, roleRightsModel);

			return Json(response);
        }
    }
}
