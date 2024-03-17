using System.Text.Json.Serialization;

namespace T052_WebAppSecurity.Data
{
	public class JwToken
	{
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; } = string.Empty;

		[JsonPropertyName("expires_at")]
		public DateTime ExpiresAt { get; set; }
	}
}
