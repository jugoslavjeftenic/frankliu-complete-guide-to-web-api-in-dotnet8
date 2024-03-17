using System.Net.Http.Headers;
using System.Text.Json;

namespace T052_WebAppSecurity.Data
{
	public class WebApiExecutor(
		IHttpClientFactory httpClientFactory,
		IConfiguration configuration
		) : IWebApiExecutor
	{
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
		private readonly IConfiguration _configuration = configuration;
		private readonly string _shirtsApiName = "ShirtsApi";
		private readonly string _authApiName = "AuthorityApi";

		public async Task<T?> InvokeGet<T>(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(_shirtsApiName);

			await AddJwtToHeader(httpClient);

			var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
			var response = await httpClient.SendAsync(request);
			await HandlePotentialError(response);

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_shirtsApiName);

			await AddJwtToHeader(httpClient);

			var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
			await HandlePotentialError(response);

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task InvokePut<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_shirtsApiName);

			await AddJwtToHeader(httpClient);

			var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
			await HandlePotentialError(response);
		}

		public async Task InvokeDelete(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(_shirtsApiName);

			await AddJwtToHeader(httpClient);

			var response = await httpClient.DeleteAsync(relativeUrl);
			await HandlePotentialError(response);
		}

		private static async Task HandlePotentialError(HttpResponseMessage httpResponse)
		{
			if (httpResponse.IsSuccessStatusCode is not true)
			{
				var errorJson = await httpResponse.Content.ReadAsStringAsync();
				throw new WebApiException(errorJson);
			}
		}

		private async Task AddJwtToHeader(HttpClient httpClient)
		{
			var clientId = _configuration.GetValue<string>("ClientId");
			var secret = _configuration.GetValue<string>("Secret");

			// Authenticate
			var authClient = _httpClientFactory.CreateClient(_authApiName);
			var response = await authClient.PostAsJsonAsync("auth", new AppCredential
			{
				ClientId = clientId ?? string.Empty,
				Secret = secret ?? string.Empty
			});
			response.EnsureSuccessStatusCode();

			// Get the JWT
			string tokenString = await response.Content.ReadAsStringAsync();
			var token = JsonSerializer.Deserialize<JwToken>(tokenString);

			// Pass the JWT to endpoints through the http headers
			httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("Bearer", token?.AccessToken);
		}
	}
}
