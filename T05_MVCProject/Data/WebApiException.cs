using System.Text.Json;

namespace T05_MVCProject.Data
{
	public class WebApiException(string errorJson) : Exception
	{
		public ErrorResponse? ErrorResponse { get; } = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
	}
}
