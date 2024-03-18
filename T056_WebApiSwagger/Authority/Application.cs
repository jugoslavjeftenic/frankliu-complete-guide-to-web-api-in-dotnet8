namespace T056_WebApiSwagger.Authority
{
	public class Application
	{
		public int ApplicationId { get; set; }
		public string ApplicationName { get; set; } = String.Empty;
		public string ClientId { get; set; } = String.Empty;
		public string Secret { get; set; } = String.Empty;
		public string Scopes { get; set; } = String.Empty;
	}
}
