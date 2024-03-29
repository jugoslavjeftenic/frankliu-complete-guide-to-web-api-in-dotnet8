﻿using Microsoft.AspNetCore.Mvc;
using T045_WebApiSecurity.Attributes;
using T045_WebApiSecurity.Data;
using T045_WebApiSecurity.Filters.ActionFilters;
using T045_WebApiSecurity.Filters.AuthFilters;
using T045_WebApiSecurity.Filters.ExceptionFilters;
using T045_WebApiSecurity.Models;

namespace T045_WebApiSecurity.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	[JwtAuthFilter]
	public class ShirtsController(ApplicationDbContext db) : ControllerBase
	{
		private readonly ApplicationDbContext _db = db;

		// Create
		[HttpPost]
		[TypeFilter(typeof(Shirt_ValidateCreateShirtFilterAttribute))]
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
