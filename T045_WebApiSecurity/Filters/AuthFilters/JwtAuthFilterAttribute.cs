using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T045_WebApiSecurity.Authority;

namespace T045_WebApiSecurity.Filters.AuthFilters
{
	public class JwtAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
	{
		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			if (context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token) is not true)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();


			if (Authenticator.VerifyToken(token, configuration.GetValue<string>("SecretKey")) is not true)
			{
				context.Result = new UnauthorizedResult();
			}
		}
	}
}
