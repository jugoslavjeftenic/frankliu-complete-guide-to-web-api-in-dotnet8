using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T016_ImplementingEndpoints.Models;
using T016_ImplementingEndpoints.Models.Repositories;

namespace T016_ImplementingEndpoints.Filters.ActionFilters
{
	public class Shirt_ValidateCreateShirtFilterAttribute : ActionFilterAttribute
	{
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
				var existingShirt =
					ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);

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
