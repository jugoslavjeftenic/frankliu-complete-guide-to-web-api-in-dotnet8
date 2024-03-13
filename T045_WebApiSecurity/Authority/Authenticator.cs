﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace T045_WebApiSecurity.Authority
{
	public static class Authenticator
	{
		public static bool Authenticate(string clientId, string secret)
		{
			var app = AppRepository.GetApplicationByClientId(clientId);
			if (app is null) return false;

			return (app.ClientId.Equals(clientId) && app.Secret.Equals(secret));
		}

		public static string CreateToken(string clientId, DateTime expiresAt, string SecretKeyString)
		{
			// Algorithm
			// Payload (claims)
			// Signing Key
			var secretKey = Encoding.ASCII.GetBytes(SecretKeyString);

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