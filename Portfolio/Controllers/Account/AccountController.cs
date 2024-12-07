using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces.Services.Account.Dtos;
using Portfolio.Core.Interfaces.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Portfolio.Web.Models.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Portfolio.Core.Entities.ClassBases;
using Portfolio.Core.Interfaces.Services.AuthenticateManagerInterfaces;
using Portfolio.Core.Interfaces.Services.CacheInterfaces;
using Portfolio.Web.Models;
using System.Security.Claims;
using Portfolio.Core.Entities.Account;
using Portfolio.Core.Common;
using Portfolio.Provider.AccountProvider;
using Portfolio.Provider.CacheProvider;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Portfolio.Web.Controllers.Account;
public class AccountController : Controller
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserService _userService;
    private readonly Core.Entities.ClassBases.IResult _result;


    public AccountController(IUserService userService, Core.Entities.ClassBases.IResult result, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _result = result;
        _contextAccessor = contextAccessor;
    }
    [HttpGet]
    public IActionResult Register()
    {
        var model = new AccountViewModel();
        return View(model);
    }
    [HttpGet]
    public IActionResult Login()
    {
        var model = new LoginDto();
        return View(model);
    }
    //[AllowAnonymous]
    //[ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> RegisterUser(AccountViewModel input)
    {
        if (!ModelState.IsValid)
        {

        }
        var result = await _userService.CreateUser(input.registerDto);
        return Json(result);

    }

 
    [AllowAnonymous]

    [HttpPost]
    public IActionResult Login(LoginDto input)
    {
        var returnUrl = "";
        var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
        if (identity.IsAuthenticated)
        {
            return Redirect(returnUrl ?? "/");
        }
        var result = _userService.Login(input);

        var browserInfo = Request.Headers["User-Agent"].ToString();
        //    _userService.SaveUserLoginAttempt(browserInfo, HttpContext.Connection.RemoteIpAddress.ToString(), result.userDto?.Id, input.UserName, result.LoginResult == LoginResult.Success);

        if (result.Result.Rsl)
        {
            if (Authenticate(result.UserDto, true))
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    returnUrl = "/";
                    return Json(new { url = returnUrl });
                }
                return Json(new { url = returnUrl });
            }
            return RedirectToAction("Index", "Home");

        }

        return Json(_result);
    }
    [AllowAnonymous]
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return Redirect("/account/login");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }


    #region Private Method
    private bool Authenticate(UserDto input, bool rememberMe = false)
    {
        var superAdmin = input.Email.Trim() == "SuperAdmin@Sarahsp.com" ? "SuperAdmin@Sarahsp.com" : string.Empty;

        var claim = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,input.Id.ToString()),
            new Claim(ClaimName.Email,input.Email),
            new Claim(ClaimName.UserId,input.Id.ToString()),

            };
        var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties
        {
            IsPersistent = rememberMe,
            ExpiresUtc = DateTime.UtcNow.AddDays(1),

        };
        HttpContext.SignInAsync(principal, properties);
        _contextAccessor.HttpContext.Session.SetString("UserId", input.Id.ToString());
        return true;
    }
    #endregion
}

//    [AllowAnonymous]

//    [HttpPost]
//    [Route("Account/login")]
//    public IActionResult Login(LoginDto input)
//    {
//        var result = _userService.Login(input);

//        var browserInfo = Request.Headers["User-Agent"].ToString();
//        _userService.SaveUserLoginAttempt(browserInfo, HttpContext.Connection.RemoteIpAddress.ToString(), result.userDto?.Id, input.UserName, result.LoginResult == LoginResult.Success);

//        switch (result.LoginResult)
//        {
//            case LoginResult.Success:
//                return RedirectToAction("GetPersonalClinicsLogin", new { prnId = result.userDto.PrnId, rememberMe = input.RememberMe, returnUrl = input.ReturnUrl });

//            case LoginResult.IncorrecrPassword:
//                _result.Rsl = false;
//                _result.Message = AllMessage.UserNameIsNotClinic;
//                return Json(_result);

//            case LoginResult.IsLock:
//                _result.Rsl = false;
//                _result.Message = AllMessage.IsLock;
//                return Json(_result);

//            case LoginResult.NotExistUser:
//                _result.Rsl = false;
//                _result.Message = AllMessage.UserNameIsNotClinic;
//                return Json(_result);

//            case LoginResult.NotActive:
//                _result.Rsl = false;
//                _result.Message = AllMessage.NotActive;
//                return Json(_result);
//        }
//        return View("~/Views/User/Login/Login.cshtml");
//    }
//    [AllowAnonymous]

//    [HttpGet]
//    public IActionResult GetPersonalClinicsLogin(long prnId, bool rememberMe, string returnUrl)
//    {
//        var loginDto = new LoginDto();
//        loginDto.PrnClncsDtos = _userService.GetActivePersonalClinicsByPrnId(prnId);
//        if (loginDto.PrnClncsDtos.Count == 1)
//        {
//            var user = _userService.GetUserByPrnId(prnId);
//            user.OrgnId = loginDto.PrnClncsDtos.FirstOrDefault().Value;
//            var phyDto = _userService.GetPhyByUserId(user.Id);
//            //if (phyDto != null && (phyDto.Spy == null || phyDto.SrvId == null))
//            //{
//            //    returnUrl = "/Profile/GetProfileUser?tabName=userInfo&insertInfo=true";
//            //}
//            if (Authenticate(user, rememberMe))
//            {
//                if (string.IsNullOrEmpty(returnUrl))
//                {
//                    returnUrl = "/";
//                    return Json(new { url = returnUrl });
//                }
//                return Json(new { url = returnUrl });
//            }
//            return RedirectToAction("Index", "Home");
//        }
//        return PartialView("~/Views/User/Login/_getClinicLogin.cshtml", loginDto);
//    }
//    [AllowAnonymous]

//    [HttpPost]
//    public IActionResult InsertPersonalClinicsLogin(LoginDto input)
//    {
//        var user = _userService.GetUserByPrnId(input.PrnId.Value);
//        if (input.ClinicId == null)
//        {
//            return Redirect("/user/login");
//        }
//        user.OrgnId = input.ClinicId;
//        if (Authenticate(user, input.RememberMe))
//        {
//            var phyDto = _userService.GetPhyByUserId(user.Id);
//            if (phyDto != null && (phyDto.Spy == null || phyDto.SrvId == null))
//            {
//                input.ReturnUrl = "/Profile/GetProfileUser?tabName=userInfo&insertInfo=true";
//            }
//            if (string.IsNullOrEmpty(input.ReturnUrl))
//            {
//                return Redirect("/");
//            }
//            return Redirect(input.ReturnUrl);
//        }
//        return RedirectToAction("Index", "Home");
//    }
//    [AllowAnonymous]
//    [HttpPost]
//    public IActionResult ForgotPassword()
//    {
//        return PartialView("~/Views/User/Login/_forgotPassword.cshtml");
//    }
//    [HttpPost]
//    public IActionResult GetPhoneNumberForgetPass(string phoneNumber)
//    {
//        return Json(_userService.SendCodeNumber(phoneNumber, true, SmsTyp.ForgotPassword));
//    }
//    [HttpPost]
//    public IActionResult ValidateCodeForgetPass(string phoneNumber, string code)
//    {
//        return Json(_userService.ValidateCode(phoneNumber, code, SmsTyp.ForgotPassword));
//    }
//    public IActionResult ResetPass(string pass, string repass)
//    {
//        return Json(_userService.ResetPass(pass, repass));
//    }

//    [AllowAnonymous]
//    [HttpGet("logout")]
//    public IActionResult Logout()
//    {
//        HttpContext.SignOutAsync();
//        return Redirect("/user/login");
//    }

//    [SataAuthorize(false, PermissionNames.UserManagment)]
//    [HttpGet]
//    public IActionResult GetUserManagment()
//    {
//        var orgnId = _session.OrgnId;
//        var model = new UserViewModel();
//        model.OrgnId = orgnId;
//        model.UserDtos = _userService.GetAllUser();
//        var usePermissionList = _userService.GetPermissionUser();
//        ViewBag.GetPermissionUser = usePermissionList;
//        return View("~/Views/User/UserManagment/UserManagment.cshtml", model);
//    }

//    [HttpGet]
//    public IActionResult GetUserPermission(long userId)
//    {
//        return Json(new
//        {
//            RoleDtos = _authenticateManagerService.GetAllUserRoleByUserId(userId),
//            UserPermissionDtos = GetUserPermissionList(userId)
//        });
//    }

//    [HttpGet]
//    public IActionResult GetUserPermissionTree(long userId = 0)
//    {
//        return Json(GetUserPermissionList(userId));
//    }

//    [HttpPost]
//    public IActionResult InsertUserPermission(long userPermissionId, long userId)
//    {
//        _authenticateManagerService.InsertUserPermission(userPermissionId, userId);
//        return Json(GetUserPermissionList(userId));
//    }
//    [SataAuthorize(false, PermissionNames.UserManagment_user_Edit)]
//    [SataAuthorize(false, PermissionNames.UserManagment_user_Add)]
//    [HttpGet]
//    public IActionResult EditUserModal(int? id = 0)
//    {
//        var prnId = _session.PrnId;
//        var orgnId = _session.OrgnId;
//        var model = new UserViewModel();
//        model.OrgnId = orgnId;
//        if (id != 0)
//        {
//            var user = _userService.GetUserById(id.Value);
//            model.UserDto = user;
//        }
//        model.ClinicDropdown = _userService.GetPersonalClinicsByPrnId(prnId.Value);
//        model.RoleDropdown = _userService.GetAllRole();
//        model.GenderDropdown = _userService.GetGender();
//        model.NationalityDropdown = _userService.GetNationality();
//        var usePermissionList = _userService.GetPermissionUser();
//        ViewBag.GetPermissionUser = usePermissionList;
//        return PartialView("~/Views/user/userManagment/_editUser.cshtml", model);
//    }
//    [HttpPost]
//    public IActionResult EditUser(UserViewModel model)
//    {
//        RemoveValidate("Phy", true);
//        RemoveValidate("Prn", true);
//        RemoveValidate("UserDtos", true);
//        RemoveValidate("userPermissionDtos", true);
//        RemoveValidate("rolePermissionDtos", true);
//        RemoveValidate("Orgn", true);
//        RemoveValidate("PermissionDto", true);
//        RemoveValidate("UserDto.Prn", true);
//        RemoveValidate("RoleDto", true);
//        RemoveValidate("UserDto.StrPassword", true);
//        RemoveValidate("UserDto.StrConfirmPassword", true);
//        RemoveValidate("UserDto.OrgnIdList", true);
//        RemoveValidate("MassageModel", true);
//        if (ModelState.IsValid)
//        {
//            var usePermissionList = _userService.GetPermissionUser();
//            ViewBag.GetPermissionUser = usePermissionList;
//            if (model.UserDto.Id != null && model.UserDto.Id != 0 && usePermissionList.Any(x => x.Permission.PermissionName == PermissionNames.UserManagment_user_Edit))
//            {
//                var result = _userService.UpdateUser(model.UserDto);
//                if (result.Rsl)
//                {
//                    TempData[AllMessage.EditSuccess] = result.Message;
//                    model.MassageModel.Result = (Result)result;
//                    return RedirectToAction("GetUserManagment");
//                }
//                else
//                {
//                    model.MassageModel.Result = (Result)result;
//                    return PartialView("~/Views/User/UserManagment/_editUser.cshtml", model);
//                }
//            }
//            else if (usePermissionList.Any(x => x.Permission.PermissionName == PermissionNames.UserManagment_user_Add))
//            {
//                model.UserDto.StrPassword = model.UserDto.UserName;
//                model.UserDto.StrConfirmPassword = model.UserDto.UserName;
//                var result = _userService.AddUser(model.UserDto, false);
//                if (result.Rsl)
//                {
//                    //model.Result = (Result)result;
//                    return RedirectToAction("GetUserManagment");
//                }
//                else
//                {
//                    //model.Result = (Result)result;
//                    return PartialView("~/Views/User/UserManagment/_editUser.cshtml", model);
//                }
//            }
//        }
//        else
//        {
//            var prnId = _session.PrnId;
//            TempData[AllMessage.ErrorMessage] = AllMessage.AllDataMostCompleteAndClear;
//            model.ClinicDropdown = _userService.GetPersonalClinicsByPrnId(prnId.Value);
//            model.RoleDropdown = _userService.GetAllRole();
//            model.GenderDropdown = _userService.GetGender();
//            model.NationalityDropdown = _userService.GetNationality();
//            return PartialView("~/Views/User/UserManagment/_editUser.cshtml", model);
//        }
//        return RedirectToAction("GetUserManagment");
//    }
//    [SataAuthorize(false, PermissionNames.UserManagment_user_status_Edit)]
//    [HttpPost]
//    public IActionResult StatusActiveUser(long id)
//    {
//        var result = _userService.StatusActiveUser(id);
//        //if (result.Rsl)
//        //{
//        //    TempData[AllMessage.EditSuccess] = result.Message;
//        //}
//        //else
//        //{
//        //    TempData[AllMessage.ErrorMessage] = AllMessage.Error;
//        //}
//        var model = new UserViewModel();
//        model.MassageModel.Result = (Result)result;
//        model.UserDtos = _userService.GetAllUser();
//        var usePermissionList = _userService.GetPermissionUser();
//        ViewBag.GetPermissionUser = usePermissionList;
//        return PartialView("~/Views/User/UserManagment/_getAllUser.cshtml", model);
//    }

//    [HttpPost]
//    public IActionResult InsertUserRole(UserViewModel input)
//    {
//        if (input.RoleDto.UserRoleId == null)
//        {
//            var userRole = _authenticateManagerService.AddUserRole(input.RoleDto.Id, input.RoleDto.UserId);
//            return Json(new
//            {
//                RoleDtos = _authenticateManagerService.GetAllUserRoleByUserId(input.RoleDto.UserId),
//                userRole
//            });
//        }
//        else
//        {
//            var userRole = _authenticateManagerService.DeleteUserRole(input.RoleDto.UserRoleId.Value);
//            return Json(new
//            {
//                RoleDtos = _authenticateManagerService.GetAllUserRoleByUserId(input.RoleDto.UserId),
//                userRole
//            });
//        }
//    }

//    [HttpPost]
//    public IActionResult SetPermissionAllUser()
//    {
//        var reuslt = _authenticateManagerService.SetPermissionForAllUser();
//        return Json(reuslt);
//    }

//    [HttpGet]
//    public IActionResult ReStart()
//    {
//        var model = new MassageModel();
//        var result = _authenticateManagerService.Restart();
//        model.Result = (Result)result;
//        return PartialView("~/Views/Shared/_SiteMessageJs.cshtml", model);
//    }

//    #region Private Method
//    private bool Authenticate(UserDto input, bool rememberMe = false)
//    {
//        var superAdmin = input.Email.Trim() == "SuperAdmin@Rahya.com" ? "SuperAdmin@Rahya.com" : string.Empty;
//        var phyDto = _userService.GetPhyByUserId(input.Id);
//        var phyDtoSpy = phyDto?.Spy ?? 0;
//        var spyImg = phyDto?.SpyImg ?? 0;
//        var orgnId = input.OrgnId ?? 0;
//        var orgnNm = (orgnId == 0) ? "" : _cacheService.GetOrgnCache().FirstOrDefault(x => x.Id == orgnId).Name;
//        var phyId = phyDto?.PrnId ?? 0;
//        var claim = new List<Claim> {
//            new Claim(ClaimTypes.NameIdentifier,input.Id.ToString()),
//            new Claim(ClaimName.Email,input.Email),
//            new Claim(ClaimName.PrnId,input.PrnId.ToString()),
//            new Claim(ClaimName.FullName,input.FullName),
//            new Claim(ClaimName.Spy,phyDtoSpy.ToString()),
//            new Claim(ClaimName.SpyImg,spyImg.ToString()),
//            new Claim(ClaimName.OrgnId,orgnId.ToString()),
//            new Claim(ClaimName.OrgnName,orgnNm),
//            new Claim(ClaimName.SuperAdmin,superAdmin),
//            new Claim(ClaimName.PhyId,phyId.ToString())

//            };
//        var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
//        var principal = new ClaimsPrincipal(identity);
//        var properties = new AuthenticationProperties
//        {
//            IsPersistent = rememberMe,
//            ExpiresUtc = DateTime.UtcNow.AddDays(1),

//        };
//        HttpContext.SignInAsync(principal, properties);
//        _contextAccessor.HttpContext.Session.SetString("UserId", input.Id.ToString());
//        return true;
//    }
//    private List<TreeViewNode> GetUserPermissionList(long userId)
//    {
//        var userPermissionDtos = _authenticateManagerService.GetPermissionByUser(userId);
//        var treeModelList = new List<TreeViewNode>();
//        foreach (var userPermission in userPermissionDtos)
//        {
//            treeModelList.Add(new TreeViewNode
//            {
//                UserPermissionId = userPermission.Id,
//                id = userPermission.PermissionId.ToString(),
//                parent = (userPermission.Permission.Pid == null) ? "#" : userPermission.Permission.Pid.ToString(),
//                text = (userPermission.IsGranted.Value == true) ? "<i onclick='insertPermission(" + userPermission.Id + "," + userId + ")' class='fa-solid fa-circle-check'> " + "<span class='fontdefualt'>" + userPermission.Permission.Description + "</span>" + "</i>"
//                : "<i onclick='insertPermission(" + userPermission.Id + "," + userId + ")' class='fa-regular fa-circle'> " + "<span class='fontdefualt'>" + userPermission.Permission.Description + "</span>" + "</i>",
//                IsActive = userPermission.IsGranted.Value
//            });
//        }
//        return treeModelList;
//    }
//    #endregion

//    public IActionResult ChangePass()
//    {
//        ChangePassDto model = new ChangePassDto();
//        return PartialView("Login/_ChangePass", model);
//    }

//    public IActionResult ChangePassSave(ChangePassDto changePassDto)
//    {
//        if (!ModelState.IsValid)
//        {
//            return PartialView("Login/_ChangePass", changePassDto);
//        }
//        var result = _userService.ChangePass(changePassDto);
//        changePassDto.Message = result.Message;
//        return PartialView("Login/_ChangePass", changePassDto);
//    }

//    public IActionResult UserLoginAttemp(string datePersian)
//    {
//        var model = new UserLoginAttemptViewModel();
//        model.datePersian = datePersian;
//        ; model.userLoginAttemptDtos = _userService.UserLoginAttempt(datePersian);
//        return View("~/Views/User/UserLogin/CurrentUserLogin.cshtml", model);
//    }
//    public IActionResult AllUserLoginAttemp(string? datePersian)
//    {
//        var model = new UserLoginAttemptViewModel();
//        model.userLoginAttemptDtos = _userService.AllUserLoginAttempt(datePersian);
//        return View("~/Views/User/UserLogin/UserLoginAttemp.cshtml", model);
//    }
//}


