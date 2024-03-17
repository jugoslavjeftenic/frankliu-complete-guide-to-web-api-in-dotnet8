using Microsoft.AspNetCore.Mvc;
using T052_WebAppSecurity.Data;
using T052_WebAppSecurity.Models;

namespace T052_WebAppSecurity.Controllers
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
				try
				{
					var response = await _webApiExecutor.InvokePost("shirts", shirt);
					if (response is not null)
					{
						return RedirectToAction(nameof(Index));
					}
				}
				catch (WebApiException ex)
				{
					HandleWebApiException(ex);
				}
			}

			return View(shirt);
		}

		public async Task<IActionResult> UpdateShirt(int shirtId)
		{
			try
			{
				var shirt = await _webApiExecutor.InvokeGet<ShirtModel>($"shirts/{shirtId}");
				if (shirt is not null)
				{
					return View(shirt);
				}
			}
			catch (WebApiException ex)
			{
				HandleWebApiException(ex);
				return View();
			}

			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateShirt(ShirtModel shirt)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await _webApiExecutor.InvokePut($"shirts/{shirt.ShirtId}", shirt);
					return RedirectToAction(nameof(Index));
				}
			}
			catch (WebApiException ex)
			{
				HandleWebApiException(ex);
			}

			return View(shirt);
		}

		public async Task<IActionResult> DeleteShirt(int shirtId)
		{
			try
			{
				await _webApiExecutor.InvokeDelete($"shirts/{shirtId}");
				return RedirectToAction(nameof(Index));
			}
			catch (WebApiException ex)
			{
				HandleWebApiException(ex);
				return View(nameof(Index), await _webApiExecutor.InvokeGet<List<ShirtModel>>("shirts"));
			}
		}

		private void HandleWebApiException(WebApiException ex)
		{
			if (ex.ErrorResponse is not null &&
				ex.ErrorResponse.Errors is not null &&
				ex.ErrorResponse.Errors.Count > 0)
			{
				foreach (var error in ex.ErrorResponse.Errors)
				{
					ModelState.AddModelError(error.Key, string.Join("; ", error.Value));
				}
			}
		}
	}
}
