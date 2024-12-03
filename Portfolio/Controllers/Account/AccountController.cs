using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces.Services.Account.Dtos;
using Portfolio.Core.Interfaces.Services.Account;
using Microsoft.AspNetCore.Authorization;
using Portfolio.Web.Models.Account;

namespace Portfolio.Web.Controllers.Account;
public class AccountController : Controller
{
	private readonly IUserService _userService;

	public AccountController(IUserService userService)
	{
		_userService = userService;
	}
	[HttpGet]
	public IActionResult Register()
	{
		var model = new AccountViewModel();
		return View( model);
	}
	[AllowAnonymous]
	[HttpPost]
	public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
	{
		var result = await _userService.CreateUser(registerDto);
		return Json(result);
	}
	[AllowAnonymous]

	//[HttpGet]
	//public IActionResult Login(string ReturnUrl)
	//{
	//	HttpContext.Session.SetString(ClaimName.Language, "En");
	//	var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
	//	if (identity.IsAuthenticated)
	//	{
	//		return Redirect(ReturnUrl ?? "/");
	//	}
	//	var loginDto = new LoginDto();
	//	loginDto.ReturnUrl = ReturnUrl;
	//	loginDto.OrgnId = (loginDto.PhyClncsDtos.FirstOrDefault() == null) ? null : loginDto.PhyClncsDtos.FirstOrDefault().OrgnId;
	//	return View("~/Views/User/Login/Login.cshtml", loginDto);
	//}
	//[AllowAnonymous]

	//[HttpPost]
	//[Route("Account/login")]
	//public IActionResult Login(LoginDto input)
	//{
	//	var result = _userService.Login(input);

	//	var browserInfo = Request.Headers["User-Agent"].ToString();
	//	_userService.SaveUserLoginAttempt(browserInfo, HttpContext.Connection.RemoteIpAddress.ToString(), result.userDto?.Id, input.UserName, result.LoginResult == LoginResult.Success);

	//	switch (result.LoginResult)
	//	{
	//		case LoginResult.Success:
	//			return RedirectToAction("GetPersonalClinicsLogin", new { prnId = result.userDto.PrnId, rememberMe = input.RememberMe, returnUrl = input.ReturnUrl });

	//		case LoginResult.IncorrecrPassword:
	//			_result.Rsl = false;
	//			_result.Message = AllMessage.UserNameIsNotClinic;
	//			return Json(_result);

	//		case LoginResult.IsLock:
	//			_result.Rsl = false;
	//			_result.Message = AllMessage.IsLock;
	//			return Json(_result);

	//		case LoginResult.NotExistUser:
	//			_result.Rsl = false;
	//			_result.Message = AllMessage.UserNameIsNotClinic;
	//			return Json(_result);

	//		case LoginResult.NotActive:
	//			_result.Rsl = false;
	//			_result.Message = AllMessage.NotActive;
	//			return Json(_result);
	//	}
	//	return View("~/Views/User/Login/Login.cshtml");
	//}
	public IActionResult AccessDenied()
	{
		return View();
	}
}



