using System.Text.Json;

namespace T05_MVCProject.Data
{
	public class WebApiExecuter(IHttpClientFactory httpClientFactory) : IWebApiExecuter
	{
		private const string _apiName = "ShirtsApi";
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

		public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);
			var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);

			await HandlePotentionalError(response);

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task<T?> InvokeGet<T>(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);
			var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
			var response = await httpClient.SendAsync(request);

			await HandlePotentionalError(response);

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task InvokePut<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);
			var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);

			await HandlePotentionalError(response);
		}

		public async Task InvokeDelete(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);
			var response = await httpClient.DeleteAsync(relativeUrl);

			await HandlePotentionalError(response);
		}

		private async Task HandlePotentionalError(HttpResponseMessage httpResponse)
		{
			if (!httpResponse.IsSuccessStatusCode)
			{
				var errorJson = await httpResponse.Content.ReadAsStringAsync();
				throw new WebApiException(errorJson);
			}

		}
	}
}
