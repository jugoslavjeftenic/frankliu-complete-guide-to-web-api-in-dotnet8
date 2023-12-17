using Microsoft.AspNetCore.Mvc;
using T02_WebAPIControllers.Models;

namespace T02_WebAPIControllers.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController : ControllerBase
	{
		private List<ShirtModel> shirts = [
			new ShirtModel
			{
				ShirtId = 1,
				Brand = "My Brand",
				Color = "Blue",
				Gender = "Men",
				Price = 30,
				Size = 10
			},
			new ShirtModel
			{
				ShirtId = 2,
				Brand = "My Brand",
				Color = "Black",
				Gender = "Men",
				Price = 35,
				Size = 12
			},
			new ShirtModel
			{
				ShirtId = 3,
				Brand = "Your Brand",
				Color = "Pink",
				Gender = "Women",
				Price = 28,
				Size = 8
			},
			new ShirtModel
			{
				ShirtId = 4,
				Brand = "Your Brand",
				Color = "Yello",
				Gender = "Women",
				Price = 30,
				Size = 9
			}
		];

		[HttpPost]
		public IActionResult CreateShirt([FromBody] ShirtModel shirt)
		{
			return Ok($"Create a shirt.");
		}

		[HttpGet]
		public IActionResult ReadAllShirts()
		{
			return Ok($"Read all shirts.");
		}

		[HttpGet("{id}")]
		public IActionResult ReadShirtById(int id)
		{
			if (id < 1)
			{
				return BadRequest();
			}

			var shirt = shirts.FirstOrDefault(x => x.ShirtId == id);

			if (shirt == null)
			{
				return NotFound();
			}

			return Ok(shirt);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateShirt(int id)
		{
			return Ok($"Update a shirt with Id: {id}.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteShirt(int id)
		{
			return Ok($"Delete a shirt with Id: {id}.");
		}
	}
}
