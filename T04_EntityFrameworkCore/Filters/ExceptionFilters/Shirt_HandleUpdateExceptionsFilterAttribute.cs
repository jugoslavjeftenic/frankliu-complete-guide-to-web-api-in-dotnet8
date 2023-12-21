using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T04_EntityFrameworkCore.Models.Repositories;

namespace T04_EntityFrameworkCore.Filters.ExceptionFilters
{
	public class Shirt_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			base.OnException(context);

			var strShirtId = context.RouteData.Values["id"] as string;
			if (int.TryParse(strShirtId, out int shirtId))
			{
				if (!ShirtRepository.ShirtExists(shirtId))
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
