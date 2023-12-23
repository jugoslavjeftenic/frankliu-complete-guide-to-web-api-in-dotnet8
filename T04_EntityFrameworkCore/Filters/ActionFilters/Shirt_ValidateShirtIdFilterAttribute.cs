using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T04_EntityFrameworkCore.Data;
using T04_EntityFrameworkCore.Models.Repositories;

namespace T04_EntityFrameworkCore.Filters.ActionFilters
{
	public class Shirt_ValidateShirtIdFilterAttribute : ActionFilterAttribute
	{
		private readonly ApplicationDbContext _db;

		public Shirt_ValidateShirtIdFilterAttribute(ApplicationDbContext db)
		{
			_db = db;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			var shirtId = context.ActionArguments["id"] as int?;
			if (shirtId.HasValue)
			{
				if (shirtId.Value < 1)
				{
					context.ModelState.AddModelError("ShirtId", "ShirtId is invalid.");
					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Status = StatusCodes.Status400BadRequest
					};
					context.Result = new BadRequestObjectResult(problemDetails);
				}
				else
				{
					var shirt = _db.Shirts.Find(shirtId.Value);

					if (shirt == null)
					{
						context.ModelState.AddModelError("ShirtId", "Shirt doesn't exists.");
						var problemDetails = new ValidationProblemDetails(context.ModelState)
						{
							Status = StatusCodes.Status404NotFound
						};
						context.Result = new NotFoundObjectResult(problemDetails);
					}
					else
					{
						context.HttpContext.Items["shirt"] = shirt;
					}
				}
			}
		}
	}
}
