using Microsoft.AspNetCore.Mvc;
using T022_EntityFrameworkCore.Data;
using T022_EntityFrameworkCore.Filters.ActionFilters;
using T022_EntityFrameworkCore.Filters.ExceptionFilters;
using T022_EntityFrameworkCore.Models;
using T022_EntityFrameworkCore.Models.Repositories;

namespace T022_EntityFrameworkCore.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ShirtsController(ApplicationDbContext db) : ControllerBase
	{
		private readonly ApplicationDbContext _db = db;

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
			return Ok(_db.Shirts.ToList());
		}

		// Update
		[HttpPut("{id}")]
		[TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
		[Shirt_ValidateUpdateShirtFilter]
		[Shirt_HandleUpdateExceptionsFilter]
		public IActionResult UpdateShirt(int id, ShirtModel shirt)
		{
			ShirtRepository.EditShirt(shirt);

			return NoContent();
		}

		// Delete
		[HttpDelete("{id}")]
		[TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
		public IActionResult DeleteShirt(int id)
		{
			var shirt = ShirtRepository.GetShirtById(id);
			ShirtRepository.DeleteShirt(id);

			return Ok(shirt);
		}

		// ReadById
		[HttpGet("{id}")]
		[TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
		public IActionResult GetShirtById(int id)
		{
			return Ok(HttpContext.Items["shirt"]);
		}
	}
}
