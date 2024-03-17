using System.Text.Json;

namespace T052_WebAppSecurity.Data
{
	public class WebApiException(string errorJson) : Exception
	{
		public ErrorResponse? ErrorResponse { get; } = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
	}
}
