using Core.Config;
using Core.Drivers;
using Core.Logging;
using log4net;
using OpenQA.Selenium;

namespace Tests.Fixtures
{
	public class WebDriverFixture : IDisposable
	{
		private readonly ILog Logger;

		public IWebDriver Driver { get; private set; }

		public WebDriverFixture()
		{
			Log4NetConfig.Configure();
			Logger = Log4NetConfig.GetLogger(GetType());
			Logger.Info("Initializing WebDriverFixture");

			Driver = DriverFactory.CreateDriver(TestConfig.Browser);
			Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestConfig.ImplicitWaitSeconds);

			Logger.Info("WebDriver initialized successfully");
		}

		public void Dispose()
		{
			Logger.Info("Disposing WebDriverFixture");

			try
			{
				Driver?.Quit();
				Driver?.Dispose();
				Logger.Info("WebDriver disposed successfully");
			}
			catch (Exception ex)
			{
				Logger.Error("Error disposing WebDriver", ex);
			}
		}
	}
}
