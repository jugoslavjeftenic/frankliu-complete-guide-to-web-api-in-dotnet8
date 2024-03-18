using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T056_WebApiSwagger.Models;

namespace T056_WebApiSwagger.Filters.ActionFilters.V2
{
	public class Shirt_EnsureDescriptionIsPresentFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			var shirt = context.ActionArguments["shirt"] as ShirtModel;
			if (shirt is not null && shirt.ValidateDescription() is false)
			{
				context.ModelState.AddModelError("Shirt", "Description is required.");
				var problemDetails = new ValidationProblemDetails(context.ModelState)
				{
					Status = StatusCodes.Status400BadRequest
				};

				context.Result = new BadRequestObjectResult(problemDetails);
			}
		}
	}
}
