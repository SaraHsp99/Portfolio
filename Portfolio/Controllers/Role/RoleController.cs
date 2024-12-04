using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces.Services.AuthenticateManagerInterfaces;

namespace Portfolio.Web.Controllers.Role
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

//using ClinicSata.Core.Common;
//using ClinicSata.Core.Interfaces.AuthenticateManagerInterface;
//using ClinicSata.Core.Interfaces.RoleInterfaces;
//using ClinicSata.Core.Securities;
//using ClinicSata.EntityFramework;
//using ClinicSata.Web.Models.RoleModels;
//using ClinicSata.Web.Models.UserModels;
//using Microsoft.AspNetCore.Mvc;

//namespace ClinicSata.Web.Controllers
//{
//    [SataAuthorize(false, PermissionNames.roleManagment)]

//    [AuditLog]
//    public class RoleController : Controller
//    {
//        private readonly IAuthenticateManagerService _authenticateManagerService;
//        private readonly IRoleService _roleService;

//        public RoleController(IAuthenticateManagerService authenticateManagerService, IRoleService roleService)
//        {
//            _authenticateManagerService = authenticateManagerService;
//            _roleService = roleService;
//        }

//        [HttpGet]
//        public IActionResult GetRoleManagment()
//        {
//            var model = new RoleViewModel();
//            model.RoleDtos = _roleService.GetAllRole();
//            //model.RolePermissionDtos = _authenticateManagerService.GetRolePermissionsByRoleId(roleId);           
//            return View("~/Views/Role/RoleManagment/RoleManagment.cshtml", model);
//        }

//        [HttpGet]
//        public IActionResult GetRolePermission(int roleId)
//        {
//            return Json(GetRolePermissionList(roleId));
//        }

//        [HttpPost]
//        public IActionResult InsertRolePermission(long rolePermissionId, int roleId)
//        {
//            var result = _authenticateManagerService.InsertRolePermission(rolePermissionId, roleId);
//            return Json(result);
//        }

//        [HttpGet]
//        public IActionResult EditRoleModal(int? id = 0)
//        {
//            var role = _roleService.GetRoleById(id.Value);
//            var model = new RoleViewModel();
//            model.RoleDto = role;
//            return PartialView("~/Views/Role/RoleManagment/_editRole.cshtml", model);
//        }

//        [HttpPost]
//        public IActionResult EditRole(RoleViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                if (model.RoleDto.Id != null && model.RoleDto.Id != 0)
//                {
//                    var result = _roleService.UpdateRole(model.RoleDto);
//                    if (result.Rsl)
//                    {
//                        TempData[AllMessage.EditSuccess] = result.Message;
//                    }
//                    else
//                    {
//                        TempData[AllMessage.ErrorMessage] = AllMessage.Error;
//                    }
//                }
//                else
//                {
//                    var result = _roleService.AddRole(model.RoleDto);
//                    if (result.Rsl)
//                    {
//                        TempData[AllMessage.SaveSuccess] = result.Message;
//                    }
//                    else
//                    {
//                        TempData[AllMessage.ErrorMessage] = AllMessage.Error;
//                    }
//                }
//            }
//            return RedirectToAction("GetRoleManagment");
//        }

//        [HttpPost]
//        public IActionResult StatusActiveRole(int roleId)
//        {
//            var result = _roleService.DeleteRole(roleId);
//            if (result.Rsl)
//            {
//                TempData[AllMessage.SuccessMessage] = result.Message;
//            }
//            else
//            {
//                TempData[AllMessage.ErrorMessage] = result.Message;
//            }
//            var model = new RoleViewModel();
//            model.RoleDtos = _roleService.GetAllRole();
//            //model.RolePermissionDtos = _authenticateManagerService.GetRolePermissionsByRoleId(roleId);           
//            return PartialView("~/Views/Role/RoleManagment/_getAllRole.cshtml", model);

//        }

//        #region Private Method

//        private List<TreeViewNode> GetRolePermissionList(int roleId)
//        {
//            var rolePermissionDtos = _authenticateManagerService.GetRolePermissionsByRoleId(roleId);
//            var treeModelList = new List<TreeViewNode>();
//            foreach (var rolePermission in rolePermissionDtos)
//            {
//                treeModelList.Add(new TreeViewNode
//                {
//                    UserPermissionId = rolePermission.Id,
//                    id = rolePermission.PermissionId.ToString(),
//                    parent = (rolePermission.Permission.Pid == null) ? "#" : rolePermission.Permission.Pid.ToString(),
//                    text = (rolePermission.IsGranted == true) ? "<i onclick='insertPermission(" + rolePermission.Id + "," + roleId + ")' class='fa-solid fa-circle-check'> " + "<span class='fontdefualt'>" + rolePermission.Permission.Description + "</span>" + "</i>"
//                    : "<i onclick='insertPermission(" + rolePermission.Id + "," + roleId + ")' class='fa-regular fa-circle'> " + "<span  class='fontdefualt'>" + rolePermission.Permission.Description + "</span>" + "</i>",
//                    IsActive = rolePermission.IsGranted.Value
//                });
//            }
//            return treeModelList;
//        }


//        #endregion
//    }
//}

