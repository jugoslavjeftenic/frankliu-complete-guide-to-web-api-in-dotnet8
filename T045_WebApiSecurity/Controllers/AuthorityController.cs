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
			if (AppRepository.Authenticate(credential.ClientId, credential.Secret))
			{
				var expiresAt = DateTime.UtcNow.AddMinutes(10);
				return Ok(new
				{
					acces_token = CreateToken(credential.ClientId, expiresAt),
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

		private string CreateToken(string clientId, DateTime expiresAt)
		{
			// Algorithm
			// Payload (claims)
			// Signing Key
			var secretKey = Encoding.ASCII
				.GetBytes(_configuration.GetValue<string>("SecretKey") ?? string.Empty);

			var app = AppRepository.GetApplicationByClientId(clientId);
			var claims = new List<Claim>()
			{
				new("AppName", app?.ApplicationName ?? string.Empty),
				new("Read", (app?.Scopes ?? string.Empty).Contains("read") ? "true" : "false"),
				new("Write", (app?.Scopes ?? string.Empty).Contains("write") ? "true" : "false")
			};

			var jwt = new JwtSecurityToken(
				signingCredentials: new SigningCredentials(
					new SymmetricSecurityKey(secretKey),
					SecurityAlgorithms.HmacSha256Signature),
				claims: claims,
				expires: expiresAt,
				notBefore: DateTime.UtcNow
				);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}
	}
}
