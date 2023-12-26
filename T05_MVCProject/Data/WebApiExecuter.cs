namespace T05_MVCProject.Data
{
	public class WebApiExecuter(IHttpClientFactory httpClientFactory) : IWebApiExecuter
	{
		private const string apiName = "ShirtsApi";
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

		public async Task<T?> InvokeGet<T>(string relativeUrl)
		{
			var httpClient = _httpClientFactory.CreateClient(apiName);
			return await httpClient.GetFromJsonAsync<T>(relativeUrl);
		}
	}
}
