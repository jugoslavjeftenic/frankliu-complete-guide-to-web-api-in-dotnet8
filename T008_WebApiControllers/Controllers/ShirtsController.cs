using Microsoft.AspNetCore.Mvc;
using T008_WebApiControllers.Models;

namespace T008_WebApiControllers.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController : ControllerBase
	{

		[HttpGet]
		public string GetShirts()
		{
			return "Reading all the shirts.";
		}

		[HttpGet("{id}")]
		public string GetShirtById(int id)
		{
			return $"Reading shirt with id: {id}.";
		}

		[HttpGet("routeColor/{color}")]
		public string GetShirtByColorFromRoute([FromRoute] string color)
		{
			return $"Reading shirt with color: {color}.";
		}

		[HttpGet("queryColor")]
		public string GetShirtByColorFromQuery([FromQuery] string color)
		{
			return $"Reading shirt with color: {color}.";
		}

		[HttpGet("headerColor")]
		public string GetShirtByColorFromHeader([FromHeader(Name = "Color")] string color)
		{
			return $"Reading shirt with color: {color}.";
		}

		[HttpPost]
		public string CreateShirt([FromBody] ShirtModel shirt)
		{
			return $"Creating a shirt ({shirt.Brand}, {shirt.Color}).";
		}

		[HttpPut("{id}")]
		public string UpdateShirt(int id, [FromForm] ShirtModel shirt)
		{
			return $"Updating shirt with id: {id} ({shirt.Brand}, {shirt.Color}).";
		}

		[HttpDelete("{id}")]
		public string DeleteShirt(int id)
		{
			return $"Deleting shirt with id: {id}.";
		}
	}
}
