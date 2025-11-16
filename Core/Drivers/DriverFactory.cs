using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Concurrent;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Core.Drivers
{
	public sealed class DriverFactory
	{
		private static readonly ConcurrentDictionary<string, Lazy<IWebDriver>> _drivers = new();

		private DriverFactory() { }

		/// <summary>
		/// Gets the singleton WebDriver instance.
		/// Creates a new instance if one doesn't exist.
		/// </summary>
		/// <returns>The singleton WebDriver instance</returns>
		public static IWebDriver GetDriver(string browserName)
		{
			return _drivers.GetOrAdd(browserName, CreateDriver).Value;
		}

		#region Create Driver

		private static Lazy<IWebDriver> CreateDriver(string browserName)
		{
			var options = DriverOptionsFactory.GetOptions(browserName);

			return browserName.ToLower() switch
			{
				"chrome" => new Lazy<IWebDriver>(() => CreateChromeDriver((ChromeOptions)options), LazyThreadSafetyMode.ExecutionAndPublication),
				"firefox" => new Lazy<IWebDriver>(() => CreateFirefoxDriver((FirefoxOptions)options), LazyThreadSafetyMode.ExecutionAndPublication),
				_ => throw new ArgumentException($"Unsupported browser: {browserName}")
			};
		}

		private static IWebDriver CreateChromeDriver(ChromeOptions options)
		{
			new DriverManager().SetUpDriver(new ChromeConfig());
			return new ChromeDriver(options);
		}

		private static IWebDriver CreateFirefoxDriver(FirefoxOptions options)
		{
			new DriverManager().SetUpDriver(new FirefoxConfig());
			return new FirefoxDriver(options);
		}

		#endregion

		public static void QuitDriver(string browserName)
		{
			if(_drivers.TryGetValue(browserName, out var webDriver))
			{
				if (webDriver.IsValueCreated)
				{
					webDriver.Value?.Quit();
					webDriver.Value?.Dispose();
				}

				_drivers.TryRemove(browserName, out _);
			}
		}

		public static bool IsDriverInitialized(string browserName)
		{
			return _drivers[browserName]?.IsValueCreated == true;
		}
	}
}
