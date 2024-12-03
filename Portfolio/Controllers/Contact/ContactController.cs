using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers.Contact
{
	public class ContactController : Controller
	{
		public IActionResult CuntactMe()
		{
			return View();
		}
	}
}
