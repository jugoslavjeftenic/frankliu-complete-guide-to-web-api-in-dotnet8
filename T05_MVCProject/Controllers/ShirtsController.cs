using Microsoft.AspNetCore.Mvc;
using T05_MVCProject.Data;
using T05_MVCProject.Models;

namespace T05_MVCProject.Controllers
{
	public class ShirtsController(IWebApiExecuter webApiExecuter) : Controller
	{
		private readonly IWebApiExecuter _webApiExecuter = webApiExecuter;

		public async Task<IActionResult> Index()
		{
			return View(await _webApiExecuter.InvokeGet<List<ShirtModel>>("shirts"));
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
				try
				{
					var response = await _webApiExecuter.InvokePost("shirts", shirt);
					if (response != null)
					{
						return RedirectToAction("Index");
					}
				}
				catch (WebApiException ex)
				{
					if (ex.ErrorResponse != null &&
						ex.ErrorResponse.Errors != null &&
						ex.ErrorResponse.Errors.Count > 0)
					{
						foreach (var error in ex.ErrorResponse.Errors)
						{
							ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
						}
					}
				}
			}

			return View(shirt);
		}

		public async Task<IActionResult> UpdateShirt(int shirtId)
		{
			var shirt = await _webApiExecuter.InvokeGet<ShirtModel>($"shirts/{shirtId}");
			if (shirt != null)
			{
				return View(shirt);
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateShirt(ShirtModel shirt)
		{
			if (ModelState.IsValid)
			{
				await _webApiExecuter.InvokePut($"shirts/{shirt.ShirtId}", shirt);
				return RedirectToAction(nameof(Index));
			}

			return View(shirt);
		}

		public async Task<IActionResult> DeleteShirt(int shirtId)
		{
			await _webApiExecuter.InvokeDelete($"shirts/{shirtId}");
			return RedirectToAction(nameof(Index));
		}
	}
}
