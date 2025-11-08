using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace Core.Drivers
{
	public static class DriverOptionsFactory
	{
		public static DriverOptions GetOptions(string browserName)
		{
			var browserLowerName = browserName.ToLower();
			DriverOptions options = browserLowerName switch
			{
				"chrome" => CreateChromeOptions(),
				"firefox" => CreateFirefoxOptions(),
				_ => throw new ArgumentException($"Unsupported browser: {browserName}")
			};

			return options;
		}

		public static DriverOptions CreateChromeOptions()
		{
			var options = new ChromeOptions();
			options.AddArgument("--start-maximized");
			options.AddArgument("--disable-notifications");
			options.AddArgument("--incognito");
			options.AddArgument("--disable-extensions");
			options.AddExcludedArgument("enable-automation");

			return options;
		}

		public static DriverOptions CreateFirefoxOptions()
		{
			var options = new FirefoxOptions();
			options.AddArgument("--width=1920");
			options.AddArgument("--height=1080");
			options.AddArgument("-private");
			options.SetPreference("dom.webnotifications.enabled", false);
			options.AcceptInsecureCertificates = true;

			return options;
		}
	}
}
