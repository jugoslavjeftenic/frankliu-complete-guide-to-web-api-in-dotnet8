namespace T031_MVCProject.Data
{
	public class WebApiExecutor(IHttpClientFactory httpClientFactory) : IWebApiExecutor
	{
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
		private readonly string apiName = "ShirtsApi";

		public async Task<T?> InvokeGet<T>(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(apiName);
			return await httpClient.GetFromJsonAsync<T>(relativeUrl);
		}
	}
}
