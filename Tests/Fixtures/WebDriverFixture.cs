using Core.Config;
using Core.Drivers;
using Core.Logging;
using log4net;
using OpenQA.Selenium;

namespace Tests.Fixtures
{
	/// <summary>
	/// Fixture for managing WebDriver lifecycle in tests.<br/>
	/// Implements IDisposable for proper resource cleanup.
	/// Uses singleton DriverFactory to share one browser instance across all tests.
	/// </summary>
	public class WebDriverFixture : IDisposable
	{
		private readonly ILog Logger;

		public static IWebDriver Driver => DriverFactory.GetDriver(TestConfig.Browser);

		public WebDriverFixture()
		{
			Log4NetConfig.Configure();
			Logger = Log4NetConfig.GetLogger(GetType());

			try
			{
				var driver = Driver;

				if (!DriverFactory.IsDriverInitialized(TestConfig.Browser))
				{
					driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestConfig.ImplicitWaitSeconds);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to initialize WebDriver", ex);
				throw;
			}
		}

		/// <summary>
		/// Disposes the WebDriver instance and performs cleanup.
		/// Closes the browser and releases all associated resources.
		/// </summary>
		public void Dispose()
		{
			try
			{
				if (DriverFactory.IsDriverInitialized(TestConfig.Browser))
				{
					DriverFactory.QuitDriver(TestConfig.Browser);
					Logger.Info("WebDriver disposed successfully");
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error disposing WebDriver", ex);
			}
			finally
			{
				GC.SuppressFinalize(this);
			}
		}
	}
}
