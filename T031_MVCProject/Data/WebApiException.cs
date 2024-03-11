using System.Text.Json;

namespace T031_MVCProject.Data
{
	public class WebApiException(string errorJson) : Exception
	{
		public ErrorResponse? ErrorResponse { get; } = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
	}
}
