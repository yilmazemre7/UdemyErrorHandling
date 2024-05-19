using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UdemyErrorHandling.Models;

namespace UdemyErrorHandling.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			int value1 = 5;
			int value2 = 0;
			int result = value1 / value2;
			return View();
		}

		public IActionResult Privacy()
		{
			throw new FileNotFoundException();
			return View();
		}

		[AllowAnonymous]
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			//ExceptionHandler Path Feature ile httpcontext yard�m�yla hata yakalan�r ard�ndan viewbag ile error viewine g�nderilir .
			var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
			ViewBag.path = exception?.Path;
			ViewBag.message = exception?.Error.Message;
			return View();
		}
	}
}
