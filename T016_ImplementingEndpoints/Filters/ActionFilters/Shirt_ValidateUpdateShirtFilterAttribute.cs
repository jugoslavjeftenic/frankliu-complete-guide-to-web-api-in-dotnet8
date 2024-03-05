using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T016_ImplementingEndpoints.Models;

namespace T016_ImplementingEndpoints.Filters.ActionFilters
{
	public class Shirt_ValidateUpdateShirtFilterAttribute : ActionFilterAttribute
	{
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
		}
	}
}
