﻿namespace T045_WebApiSecurity.Authority
{
	public static class AppRepository
	{
		private static readonly List<Application> _applications =
		[
			new Application
			{
				ApplicationId = 1,
				ApplicationName = "MVCWebApp",
				ClientId = "53D3C1E6-4587-4AD5-8C6E-A8E4BD59940E",
				Secret = "0673FC70-0514-4011-B4A3-DF9BC03201BC",
				Scopes = "read,write,delete"
			}
		];

		public static Application? GetApplicationByClientId(string clientId)
		{
			return _applications.FirstOrDefault(x => x.ClientId.Equals(clientId));
		}
	}
}
