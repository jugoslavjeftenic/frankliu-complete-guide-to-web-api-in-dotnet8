namespace T05_MVCProject.Data
{
	public class WebApiExecuter(IHttpClientFactory httpClientFactory) : IWebApiExecuter
	{
		private const string _apiName = "ShirtsApi";
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

		public async Task<T?> InvokeGet<T>(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);
			return await httpClient.GetFromJsonAsync<T>(relativeUrl);
		}

		public async Task<T?> InvokePost<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);
			var response = await httpClient.PostAsJsonAsync(relativeUrl, obj);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadFromJsonAsync<T>();
		}

		public async Task InvokePut<T>(string relativeUrl, T obj)
		{
			var httpClient = _httpClientFactory.CreateClient(_apiName);
			var response = await httpClient.PutAsJsonAsync(relativeUrl, obj);
			response.EnsureSuccessStatusCode();

		}
	}
}
