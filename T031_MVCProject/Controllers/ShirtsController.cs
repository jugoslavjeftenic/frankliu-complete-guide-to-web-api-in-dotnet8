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
			return View(await _webApiExecutor.InvokeGet<List<ShirtModel>>("https://localhost:7294/api/v1/shirts"));
			//return View(await _webApiExecutor.InvokeGet<List<ShirtModel>>("shirts"));
		}
	}
}
