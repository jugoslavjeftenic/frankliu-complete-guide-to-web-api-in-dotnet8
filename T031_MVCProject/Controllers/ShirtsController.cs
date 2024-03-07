using Microsoft.AspNetCore.Mvc;
using T031_MVCProject.Models.Repositories;

namespace T031_MVCProject.Controllers
{
	public class ShirtsController : Controller
	{
		public IActionResult Index()
		{
			return View(ShirtRepository.GetShirts());
		}
	}
}
