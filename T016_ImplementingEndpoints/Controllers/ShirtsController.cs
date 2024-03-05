using Microsoft.AspNetCore.Mvc;
using T016_ImplementingEndpoints.Filters.ActionFilters;
using T016_ImplementingEndpoints.Models;
using T016_ImplementingEndpoints.Models.Repositories;

namespace T016_ImplementingEndpoints.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController : ControllerBase
	{
		// Create
		[HttpPost]
		[Shirt_ValidateCreateShirtFilter]
		public IActionResult CreateShirt(ShirtModel shirt)
		{
			ShirtRepository.AddShirt(shirt);

			return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
		}

		// Read
		[HttpGet]
		public IActionResult GetShirts()
		{
			return Ok(ShirtRepository.GetShirts());
		}

		// Update
		[HttpPut("{id}")]
		[Shirt_ValidateShirtIdFilter]
		[Shirt_ValidateUpdateShirtFilter]
		public IActionResult UpdateShirt(int id, ShirtModel shirt)
		{
			try
			{
				ShirtRepository.EditShirt(shirt);
			}
			catch (Exception)
			{
				if (ShirtRepository.ShirtExists(id) is not true) return NotFound();
				throw;
			}

			return NoContent();
		}

		[HttpGet("{id}")]
		[Shirt_ValidateShirtIdFilter]
		public IActionResult GetShirtById(int id)
		{
			return Ok(ShirtRepository.GetShirtById(id));
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
		[HttpDelete("{id}")]
		public IActionResult DeleteShirt(int id)
		{
			return Ok($"Deleting shirt with id: {id}.");
		}
	}
}
