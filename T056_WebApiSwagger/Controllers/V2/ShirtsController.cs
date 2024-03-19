using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using T056_WebApiSwagger.Attributes;
using T056_WebApiSwagger.Data;
using T056_WebApiSwagger.Filters.ActionFilters;
using T056_WebApiSwagger.Filters.ActionFilters.V2;
using T056_WebApiSwagger.Filters.AuthFilters;
using T056_WebApiSwagger.Filters.ExceptionFilters;
using T056_WebApiSwagger.Models;

namespace T056_WebApiSwagger.Controllers.V2
{
	[ApiVersion(2.0)]
	[ApiController]
	[Route("api/v{v:apiVersion}/[controller]")]
	[JwtAuthFilter]
	public class ShirtsController(ApplicationDbContext db) : ControllerBase
	{
		private readonly ApplicationDbContext _db = db;

		// Create
		[HttpPost]
		[TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
		[Shirt_EnsureDescriptionIsPresentFilter]
		[RequiredClaim("write", "true")]
		public IActionResult CreateShirt(ShirtModel shirt)
		{
			_db.Shirts.Add(shirt);
			_db.SaveChanges();

			return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
		}

		// Read
		[HttpGet]
		[RequiredClaim("read", "true")]
		public IActionResult GetShirts()
		{
			return Ok(_db.Shirts.ToList());
		}

		// Update
		[HttpPut("{id}")]
		[TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
		[TypeFilter(typeof(Shirt_ValidateUpdateShirtFilterAttribute))]
		[TypeFilter(typeof(Shirt_HandleUpdateExceptionsFilterAttribute))]
		[Shirt_EnsureDescriptionIsPresentFilter]
		[RequiredClaim("write", "true")]
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
		[RequiredClaim("delete", "true")]
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
		[RequiredClaim("read", "true")]
		public IActionResult GetShirtById(int id)
		{
			return Ok(HttpContext.Items["shirt"]);
		}
	}
}
