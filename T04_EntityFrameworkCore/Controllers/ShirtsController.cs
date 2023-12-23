using Microsoft.AspNetCore.Mvc;
using T04_EntityFrameworkCore.Data;
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
		private readonly ApplicationDbContext _db;

		public ShirtsController(ApplicationDbContext db)
		{
			_db = db;
		}

		// Create
		[HttpPost]
		[TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
		public IActionResult CreateShirt([FromBody] ShirtModel shirt)
		{
			_db.Shirts.Add(shirt);
			_db.SaveChanges();

			return CreatedAtAction(nameof(GetShirtById),
				new { id = shirt.ShirtId + 1 },
				shirt);
		}

		// Read
		[HttpGet]
		public IActionResult GetAllShirts()
		{
			return Ok(_db.Shirts.ToList());
		}

		// Read
		[HttpGet("{id}")]
		[TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
		public IActionResult GetShirtById(int id)
		{
			return Ok(HttpContext.Items["shirt"]);
		}

		// Update
		[HttpPut("{id}")]
		[TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
		[Shirt_ValidateUpdateShirtFilter]
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
	}
}
