using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers.Skills
{
	public class SkillsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
