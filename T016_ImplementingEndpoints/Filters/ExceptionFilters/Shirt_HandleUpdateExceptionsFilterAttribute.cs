﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T016_ImplementingEndpoints.Models.Repositories;

namespace T016_ImplementingEndpoints.Filters.ExceptionFilters
{
	public class Shirt_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			base.OnException(context);

			var shirtIdString = context.RouteData.Values["id"] as string;
			if (int.TryParse(shirtIdString, out int shirtId))
			{
				if (ShirtRepository.ShirtExists(shirtId) is not true)
				{
					context.ModelState.AddModelError("ShirtId", "Shirt doesn't exist anymore.");
					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Status = StatusCodes.Status404NotFound
					};

					context.Result = new NotFoundObjectResult(problemDetails);
				}
			}
		}
	}
}
