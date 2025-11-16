using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Core.Drivers
{
	public sealed class DriverFactory
	{
		private static Lazy<IWebDriver>? _lazyDriver;
		private static readonly object _lock = new object();

		private DriverFactory() { }

		/// <summary>
		/// Gets the singleton WebDriver instance.
		/// Creates a new instance if one doesn't exist.
		/// </summary>
		/// <returns>The singleton WebDriver instance</returns>
		public static IWebDriver GetDriver(string browserName)
		{
			if (_lazyDriver == null)
			{
				lock (_lock)
				{
					if (_lazyDriver == null)
					{
						_lazyDriver = new Lazy<IWebDriver>(
							() => CreateDriver(browserName),
							LazyThreadSafetyMode.ExecutionAndPublication
						);
					}
				}
			}
			return _lazyDriver.Value;
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

		public static void QuitDriver()
		{
			lock (_lock)
			{
				if (_lazyDriver?.IsValueCreated == true)
				{
					_lazyDriver.Value?.Quit();
					_lazyDriver.Value?.Dispose();
				}
				_lazyDriver = null;
			}
		}

		public static bool IsDriverInitialized()
		{
			return _lazyDriver?.IsValueCreated == true;
		}
	}
}
