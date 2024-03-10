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
			return View(await _webApiExecutor.InvokeGet<List<ShirtModel>>("shirts"));
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
				var response = await _webApiExecutor.InvokePost("shirts", shirt);
				if (response is not null)
				{
					return RedirectToAction(nameof(Index));
				}
			}

			return View(shirt);
		}

		public async Task<IActionResult> UpdateShirt(int shirtId)
		{
			var shirt = await _webApiExecutor.InvokeGet<ShirtModel>($"shirts/{shirtId}");
			if (shirt is not null)
			{
				return View(shirt);
			}

			return NotFound();
		}
	}
}
