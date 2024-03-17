namespace T052_WebAppSecurity.Data
{
	public class WebApiExecutor(IHttpClientFactory httpClientFactory) : IWebApiExecutor
	{
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
		private readonly string _apiName = "ShirtsApi";

		public async Task<T?> InvokeGet<T>(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);

			var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
			var response = await httpClient.SendAsync(request);
			await HandlePotentialError(response);

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);

			var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
			await HandlePotentialError(response);

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task InvokePut<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);

			var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
			await HandlePotentialError(response);
		}

		public async Task InvokeDelete(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);

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
	}
}
