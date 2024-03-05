using Microsoft.AspNetCore.Mvc;
using T022_EntityFrameworkCore.Filters.ActionFilters;
using T022_EntityFrameworkCore.Filters.ExceptionFilters;
using T022_EntityFrameworkCore.Models;
using T022_EntityFrameworkCore.Models.Repositories;

namespace T022_EntityFrameworkCore.Controllers
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
		[Shirt_HandleUpdateExceptionsFilter]
		public IActionResult UpdateShirt(int id, ShirtModel shirt)
		{
			ShirtRepository.EditShirt(shirt);

			return NoContent();
		}

		// Delete
		[HttpDelete("{id}")]
		[Shirt_ValidateShirtIdFilter]
		public IActionResult DeleteShirt(int id)
		{
			var shirt = ShirtRepository.GetShirtById(id);
			ShirtRepository.DeleteShirt(id);

			return Ok(shirt);
		}

		// ReadById
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
	}
}
