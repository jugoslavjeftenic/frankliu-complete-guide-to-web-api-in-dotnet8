using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T022_EntityFrameworkCore.Data;

namespace T022_EntityFrameworkCore.Filters.ExceptionFilters
{
	public class Shirt_HandleUpdateExceptionsFilterAttribute(ApplicationDbContext db) : ExceptionFilterAttribute
	{
		private readonly ApplicationDbContext _db = db;

		public override void OnException(ExceptionContext context)
		{
			base.OnException(context);

			var shirtIdString = context.RouteData.Values["id"] as string;
			if (int.TryParse(shirtIdString, out int shirtId))
			{
				if (_db.Shirts.FirstOrDefault(x => x.ShirtId.Equals(shirtId)) is null)
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
