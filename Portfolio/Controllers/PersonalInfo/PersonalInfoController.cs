using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers.PersonalInfo
{
	public class PersonalInfoController : Controller
	{
		public IActionResult GetPersonalInfo()
		{
			//var personalInfo = _context.PersonalInfos.FirstOrDefault();
			return View("~/Views/PersonalInfo/PersonalInfo.cshtml");
		}
	}
}
