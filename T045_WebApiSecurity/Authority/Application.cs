namespace T045_WebApiSecurity.Authority
{
	public class Application
	{
		public int ApplicationId { get; set; }
		public string ApplicationName { get; set; } = String.Empty;
		public string ClientId { get; set; } = String.Empty;
		public string Secret { get; set; } = String.Empty;
	}
}
