using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using T045_WebApiSecurity.Attributes;
using T045_WebApiSecurity.Authority;

namespace T045_WebApiSecurity.Filters.AuthFilters
{
	[AttributeUsage(AttributeTargets.All)]
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
			var secretKey = configuration?.GetValue<string>("SecretKey");

			if (string.IsNullOrEmpty(secretKey))
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			var claims = await Task.Run(() => Authenticator.VerifyToken(token, secretKey));

			if (claims is null)
			{
				context.Result = new UnauthorizedResult(); // 401
			}
			else
			{
				var requiredClaims = context.ActionDescriptor.EndpointMetadata
					.OfType<RequiredClaimAttribute>()
					.ToList();

				// 403
				if (requiredClaims is not null &&
					requiredClaims.All(
						rc => claims.Any(c =>
							c.Type.ToLower().Equals(rc.ClaimType.ToLower()) &&
							c.Value.ToLower().Equals(rc.ClaimValue.ToLower())
						)
					) is not true)
				{
					context.Result = new StatusCodeResult(403);
				}
			}
		}
	}
}
