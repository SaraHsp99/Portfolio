using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers.Account;
public class AccountController : Controller
{
	public IActionResult Register()
	{
		return View();
	}
	public IActionResult Login()
	{
		return View();
	}
	public IActionResult Logout()
	{
		return View();
	}
	public IActionResult AccessDenied()
	{
		return View();
	}
}

