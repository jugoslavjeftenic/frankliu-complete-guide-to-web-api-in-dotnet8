using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T04_EntityFrameworkCore.Models;

namespace T04_EntityFrameworkCore.Filters.ActionFilters
{
	public class Shirt_ValidateUpdateShirtFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			var id = context.ActionArguments["id"] as int?;
			var shirt = context.ActionArguments["shirt"] as ShirtModel;
			if (id.HasValue && shirt != null && id != shirt.ShirtId)
			{
				context.ModelState.AddModelError("ShirtId", "ShirtId is not the same as id.");
				var problemDetails = new ValidationProblemDetails(context.ModelState)
				{
					Status = StatusCodes.Status400BadRequest
				};
				context.Result = new BadRequestObjectResult(problemDetails);

			}
		}
	}
}
