using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using T045_WebApiSecurity.Authority;

namespace T045_WebApiSecurity.Controllers
{
	[ApiController]
	public class AuthorityController(IConfiguration configuration) : ControllerBase
	{
		private readonly IConfiguration _configuration = configuration;

		[HttpPost("auth")]
		public IActionResult Authenticate([FromBody] AppCredential credential)
		{
			if (Authenticator.Authenticate(credential.ClientId, credential.Secret))
			{
				var expiresAt = DateTime.UtcNow.AddMinutes(10);
				return Ok(new
				{
					acces_token = Authenticator.CreateToken(
						credential.ClientId, expiresAt,
						_configuration.GetValue<string>("SecretKey") ?? string.Empty
						),
					expires_at = expiresAt
				});
			}
			else
			{
				ModelState.AddModelError("Unauthorized", "You are not authorized.");
				var problemDetails = new ValidationProblemDetails(ModelState)
				{
					Status = StatusCodes.Status401Unauthorized
				};

				return new UnauthorizedObjectResult(problemDetails);
			}
		}
	}
}
