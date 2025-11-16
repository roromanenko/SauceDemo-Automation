using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Core.Drivers
{
	public sealed class DriverFactory
	{
		private static readonly Dictionary<string, Lazy<IWebDriver>> _drivers = new();
		private static readonly object _lock = new object();

		private DriverFactory() { }

		/// <summary>
		/// Gets the singleton WebDriver instance.
		/// Creates a new instance if one doesn't exist.
		/// </summary>
		/// <returns>The singleton WebDriver instance</returns>
		public static IWebDriver GetDriver(string browserName)
		{
			if (!_drivers.ContainsKey(browserName))
			{
				lock (_lock)
				{
					if (!_drivers.ContainsKey(browserName))
					{
						var driver = new Lazy<IWebDriver>(
							() => CreateDriver(browserName),
							LazyThreadSafetyMode.ExecutionAndPublication
						);

						_drivers.Add(browserName, driver);
					}
				}
			}

			return _drivers[browserName].Value;
		}

		#region Create Driver

		private static IWebDriver CreateDriver(string browserName)
		{
			var options = DriverOptionsFactory.GetOptions(browserName);

			return browserName.ToLower() switch
			{
				"chrome" => CreateChromeDriver((ChromeOptions)options),
				"firefox" => CreateFirefoxDriver((FirefoxOptions)options),
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
			lock (_lock)
			{
				if (_drivers[browserName]?.IsValueCreated == true)
				{
					_drivers[browserName].Value?.Quit();
					_drivers[browserName].Value?.Dispose();
				}
				_drivers.Remove(browserName);
			}
		}

		public static bool IsDriverInitialized(string browserName)
		{
			return _drivers[browserName]?.IsValueCreated == true;
		}
	}
}
