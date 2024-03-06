using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Drawing;
using System.Reflection;
using T022_EntityFrameworkCore.Data;
using T022_EntityFrameworkCore.Models;
using T022_EntityFrameworkCore.Models.Repositories;

namespace T022_EntityFrameworkCore.Filters.ActionFilters
{
	public class Shirt_ValidateCreateShirtFilterAttribute(ApplicationDbContext db) : ActionFilterAttribute
	{
		private readonly ApplicationDbContext _db = db;

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			if (context.ActionArguments["shirt"] is not ShirtModel shirt)
			{
				context.ModelState.AddModelError("Shirt", "Shirt object is null.");
				var problemDetails = new ValidationProblemDetails(context.ModelState)
				{
					Status = StatusCodes.Status400BadRequest
				};

				context.Result = new BadRequestObjectResult(problemDetails);
			}
			else
			{
				var existingShirt = _db.Shirts.FirstOrDefault(
					x =>
						string.IsNullOrWhiteSpace(shirt.Brand) == false
						&& string.IsNullOrWhiteSpace(x.Brand) == false
						&& x.Brand.ToLower().Equals(shirt.Brand.ToLower())
						&& string.IsNullOrWhiteSpace(shirt.Gender) == false
						&& string.IsNullOrWhiteSpace(x.Gender) == false
						&& x.Gender.ToLower().Equals(shirt.Gender.ToLower())
						&& string.IsNullOrWhiteSpace(shirt.Color) == false
						&& string.IsNullOrWhiteSpace(x.Color) == false
						&& x.Color.ToLower().Equals(shirt.Color.ToLower())
						&& x.Size.Equals(shirt.Size)
					);

				if (existingShirt is not null)
				{
					context.ModelState.AddModelError("Shirt", "Shirt already exists.");
					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Status = StatusCodes.Status400BadRequest
					};

					context.Result = new BadRequestObjectResult(problemDetails);
				}
			}
		}
	}
}
