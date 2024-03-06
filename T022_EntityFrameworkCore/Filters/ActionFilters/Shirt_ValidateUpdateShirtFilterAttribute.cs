using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T022_EntityFrameworkCore.Data;
using T022_EntityFrameworkCore.Models;

namespace T022_EntityFrameworkCore.Filters.ActionFilters
{
	public class Shirt_ValidateUpdateShirtFilterAttribute(ApplicationDbContext db) : ActionFilterAttribute
	{
		private readonly ApplicationDbContext _db = db;

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			var id = context.ActionArguments["id"] as int?;
			var shirt = context.ActionArguments["shirt"] as ShirtModel;

			if (id.HasValue && shirt is not null && id.Equals(shirt.ShirtId) is not true)
			{
				context.ModelState.AddModelError("Shirt", "ShirtId is not equal to id.");
				var problemDetails = new ValidationProblemDetails(context.ModelState)
				{
					Status = StatusCodes.Status400BadRequest
				};

				context.Result = new BadRequestObjectResult(problemDetails);
			}

			if (shirt is not null)
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
