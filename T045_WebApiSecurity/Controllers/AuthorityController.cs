using Microsoft.AspNetCore.Mvc;
using T045_WebApiSecurity.Authority;

namespace T045_WebApiSecurity.Controllers
{
	[ApiController]
	public class AuthorityController : ControllerBase
	{
		[HttpPost("auth")]
		public IActionResult Authenticate([FromBody] AppCredential credential)
		{
			if (AppRepository.Authenticate(credential.ClientId, credential.Secret))
			{
				return Ok(new
				{
					acces_token = CreateToken(credential.ClientId),
					expires_at = DateTime.UtcNow.AddMinutes(10)
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

		private string CreateToken(string clientId)
		{
			return string.Empty;
		}
	}
}
