using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Core.Config
{
	/// <summary>
	/// Provides centralized configuration management for test automation settings. <br/>
	/// Reads configuration values from appsettings.json with fallback defaults.
	/// </summary>
	public static class TestConfig
	{
		private static readonly IConfigurationRoot _config =
			new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

		public static string Browser =>
			_config["TestSettings:Browser"] ?? "chrome";

		public static string BaseUrl =>
			_config["TestSettings:BaseUrl"] ?? "https://www.saucedemo.com/";

		public static int ImplicitWaitSeconds =>
			int.TryParse(_config["TestSettings:ImplicitWaitSeconds"], out var seconds)
				? seconds
				: 5;
	}
}
