﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T056_WebApiSwagger.Data;

namespace T056_WebApiSwagger.Filters.ActionFilters
{
	public class Shirt_ValidateShirtIdFilterAttribute(ApplicationDbContext db) : ActionFilterAttribute
	{
		private readonly ApplicationDbContext _db = db;

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

					if (shirt is null)
					{
						context.ModelState.AddModelError("ShirtId", "Shirt doesn't exist.");
						var problemDetails = new ValidationProblemDetails(context.ModelState)
						{
							Status = StatusCodes.Status404NotFound
						};

						context.Result = new BadRequestObjectResult(problemDetails);
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
