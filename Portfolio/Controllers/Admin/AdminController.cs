using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers.Admin
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
