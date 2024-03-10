using Microsoft.AspNetCore.Mvc;
using T031_MVCProject.Data;
using T031_MVCProject.Models;

namespace T031_MVCProject.Controllers
{
	public class ShirtsController(IWebApiExecutor webApiExecutor) : Controller
	{
		private readonly IWebApiExecutor _webApiExecutor = webApiExecutor;

		public async Task<IActionResult> Index()
		{
			return View(await _webApiExecutor.InvokeGet<List<ShirtModel>>("Shirts"));
		}

		public IActionResult CreateShirt()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateShirt(ShirtModel shirt)
		{
			if (ModelState.IsValid)
			{
				var response = await _webApiExecutor.InvokePost("Shirts", shirt);
				if (response is not null)
				{
					return RedirectToAction(nameof(Index));
				}
			}

			return View(shirt);
		}
	}
}
