using Microsoft.AspNetCore.Mvc;
using T04_EntityFrameworkCore.Filters.ActionFilters;
using T04_EntityFrameworkCore.Filters.ExceptionFilters;
using T04_EntityFrameworkCore.Models;
using T04_EntityFrameworkCore.Models.Repositories;

namespace T04_EntityFrameworkCore.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController : ControllerBase
	{
		// Create
		[HttpPost]
		[Shirt_ValidateCreateShirtFilter]
		public IActionResult CreateShirt([FromBody] ShirtModel shirt)
		{
			ShirtRepository.AddShirt(shirt);

			return CreatedAtAction(nameof(GetShirtById),
				new { id = shirt.ShirtId + 1 },
				shirt);
		}

		// Read
		[HttpGet]
		public IActionResult GetAllShirts()
		{
			return Ok(ShirtRepository.GetShirts());
		}

		// Read
		[HttpGet("{id}")]
		[Shirt_ValidateShirtIdFilter]
		public IActionResult GetShirtById(int id)
		{
			return Ok(ShirtRepository.GetShirtById(id));
		}

		// Update
		[HttpPut("{id}")]
		[Shirt_ValidateShirtIdFilter]
		[Shirt_ValidateUpdateShirtFilter]
		[Shirt_HandleUpdateExceptionsFilter]
		public IActionResult UpdateShirt(int id, ShirtModel shirt)
		{
			ShirtRepository.UpdateShirt(shirt);

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
	}
}
