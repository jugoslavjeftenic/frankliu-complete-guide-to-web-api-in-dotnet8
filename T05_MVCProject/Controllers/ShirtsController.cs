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
	}
}
