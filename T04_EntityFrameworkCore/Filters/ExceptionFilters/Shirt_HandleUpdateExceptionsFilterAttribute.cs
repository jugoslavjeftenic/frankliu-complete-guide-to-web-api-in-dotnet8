using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T04_EntityFrameworkCore.Data;
using T04_EntityFrameworkCore.Models.Repositories;

namespace T04_EntityFrameworkCore.Filters.ExceptionFilters
{
	public class Shirt_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
	{
		private readonly ApplicationDbContext _db;

		public Shirt_HandleUpdateExceptionsFilterAttribute(ApplicationDbContext db)
		{
			_db = db;
		}

		public override void OnException(ExceptionContext context)
		{
			base.OnException(context);

			var strShirtId = context.RouteData.Values["id"] as string;
			if (int.TryParse(strShirtId, out int shirtId))
			{
				if (_db.Shirts.FirstOrDefault(x => x.ShirtId == shirtId) == null)
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
