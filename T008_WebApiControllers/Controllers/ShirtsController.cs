using Microsoft.AspNetCore.Mvc;
using T008_WebApiControllers.Models;

namespace T008_WebApiControllers.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController : ControllerBase
	{
		private readonly List<ShirtModel> _shirts =
		[
			new ShirtModel{ ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Men", Price = 30, Size = 10 },
			new ShirtModel{ ShirtId = 2, Brand = "My Brand", Color = "Black", Gender = "Men", Price = 35, Size = 12 },
			new ShirtModel{ ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "Women", Price = 28, Size = 8 },
			new ShirtModel{ ShirtId = 4, Brand = "Your Brand", Color = "Yello", Gender = "Women", Price = 30, Size = 9 }
		];

		[HttpGet]
		public IActionResult GetShirts()
		{
			return Ok("Reading all the shirts.");
		}

		[HttpGet("{id}")]
		public IActionResult GetShirtById(int id)
		{
			if (id < 1)
			{
				return BadRequest();
			}

			var shirt = _shirts.FirstOrDefault(x => x.ShirtId == id);

			if (shirt is null)
			{
				return NotFound();
			}

			return Ok(shirt);
		}

		[HttpGet("routeColor/{color}")]
		public IActionResult GetShirtByColorFromRoute([FromRoute] string color)
		{
			return Ok($"Reading shirt with color: {color}.");
		}

		[HttpGet("queryColor")]
		public IActionResult GetShirtByColorFromQuery([FromQuery] string color)
		{
			return Ok($"Reading shirt with color: {color}.");
		}

		[HttpGet("headerColor")]
		public IActionResult GetShirtByColorFromHeader([FromHeader(Name = "Color")] string color)
		{
			return Ok($"Reading shirt with color: {color}.");
		}

		[HttpPost]
		public IActionResult CreateShirt([FromBody] ShirtModel shirt)
		{
			return Ok($"Creating a shirt ({shirt.Brand}, {shirt.Color}).");
		}

		[HttpPut("{id}")]
		public IActionResult UpdateShirt(int id, [FromForm] ShirtModel shirt)
		{
			return Ok($"Updating shirt with id: {id} ({shirt.Brand}, {shirt.Color}).");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteShirt(int id)
		{
			return Ok($"Deleting shirt with id: {id}.");
		}
	}
}
