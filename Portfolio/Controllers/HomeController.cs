using Microsoft.AspNetCore.Mvc;
using Portfolio.Core.Interfaces.Services.SummaryInrerfaces;
using Portfolio.Models;
using Portfolio.Web.Models;
using System.Diagnostics;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ISummaryService _summaryService;
		public HomeController(ILogger<HomeController> logger, ISummaryService summaryService)
		{
			_logger = logger;
			_summaryService = summaryService;
		}

		public async Task<IActionResult> Index()
		{
			var model = new HomeViewModel();
			model.Summary = await _summaryService.GetSummaryAsync();
			return View(model);
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


		
	}
}
