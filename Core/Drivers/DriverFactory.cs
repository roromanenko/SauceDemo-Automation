using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Core.Drivers
{
	public static class DriverFactory
	{
		public static IWebDriver CreateDriver(string browserName)
		{
			var options = DriverOptionsFactory.GetOptions(browserName);

			return browserName.ToLower() switch
			{
				"chrome" => new ChromeDriver((ChromeOptions)options),
				"firefox" => new FirefoxDriver((FirefoxOptions)options),
				_ => throw new ArgumentException($"Unsupported browser: {browserName}")
			};
		}
	}
}
