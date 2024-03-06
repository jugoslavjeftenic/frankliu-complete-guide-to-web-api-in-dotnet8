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
		[TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
		public IActionResult CreateShirt(ShirtModel shirt)
		{
			_db.Shirts.Add(shirt);
			_db.SaveChanges();

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
		[TypeFilter(typeof(Shirt_ValidateUpdateShirtFilterAttribute))]
		[TypeFilter(typeof(Shirt_HandleUpdateExceptionsFilterAttribute))]
		public IActionResult UpdateShirt(int id, ShirtModel shirt)
		{
			var shirtToUpdate = HttpContext.Items["shirt"] as ShirtModel;
			shirtToUpdate!.Brand = shirt.Brand;
			shirtToUpdate.Price = shirt.Price;
			shirtToUpdate.Size = shirt.Size;
			shirtToUpdate.Color = shirt.Color;
			shirtToUpdate.Gender = shirt.Gender;

			_db.SaveChanges();

			return NoContent();
		}

		// Delete
		[HttpDelete("{id}")]
		[TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
		public IActionResult DeleteShirt(int id)
		{
			var shirtToDelete = HttpContext.Items["shirt"] as ShirtModel;
			_db.Shirts.Remove(shirtToDelete!);
			_db.SaveChanges();

			return Ok(shirtToDelete);
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
