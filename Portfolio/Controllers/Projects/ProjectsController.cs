using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers.Projects
{
	public class ProjectsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
