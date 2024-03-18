using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace T056_WebApiSwagger.Authority
{
	public class Authenticator
	{
		public static bool Authenticate(string clientId, string secret)
		{
			var app = AppRepository.GetApplicationByClientId(clientId);
			if (app is null) return false;

			return (app.ClientId.Equals(clientId) && app.Secret.Equals(secret));
		}

		public static string CreateToken(string clientId, DateTime expiresAt, string SecretKeyString)
		{
			var secretKey = Encoding.ASCII.GetBytes(SecretKeyString);

			var app = AppRepository.GetApplicationByClientId(clientId);
			var claims = new List<Claim>
			{
				new("AppName", app?.ApplicationName ?? string.Empty),
			};

			var scopes = app?.Scopes.Split(",");
			if (scopes is not null && scopes.Length > 0)
			{
				foreach (var scope in scopes)
				{
					claims.Add(new Claim(scope.ToLower(), "true"));
				}
			}

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

		public static IEnumerable<Claim>? VerifyToken(string? token, string SecretKeyString)
		{
			if (string.IsNullOrWhiteSpace(token)) return null;

			if (token.StartsWith("Bearer"))
			{
				token = token[6..].Trim();
			}
			var secretKey = Encoding.ASCII.GetBytes(SecretKeyString);

			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretKey),
					ValidateLifetime = true,
					ValidateAudience = false,
					ValidateIssuer = false,
					ClockSkew = TimeSpan.Zero
				},
				out SecurityToken securityToken);

				if (securityToken is not null)
				{
					var tokenObject = tokenHandler.ReadJwtToken(token);
					return tokenObject.Claims ?? ([]);
				}
				else
				{
					return null;
				}
			}
			catch (SecurityTokenException)
			{
				return null;
			}
			catch
			{
				throw;
			}
		}
	}
}
