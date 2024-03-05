﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T022_EntityFrameworkCore.Models.Repositories;

namespace T022_EntityFrameworkCore.Filters.ActionFilters
{
	public class Shirt_ValidateShirtIdFilterAttribute : ActionFilterAttribute
	{
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
				else if (ShirtRepository.ShirtExists(shirtId.Value) is not true)
				{
					context.ModelState.AddModelError("ShirtId", "Shirt doesn't exist.");

					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Status = StatusCodes.Status404NotFound
					};

					context.Result = new BadRequestObjectResult(problemDetails);
				}
			}
		}
	}
}
