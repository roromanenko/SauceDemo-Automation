using Core.Logging;
using log4net;
using OpenQA.Selenium;
using Tests.Fixtures;
using Xunit.Abstractions;

namespace Tests.Tests
{
	public abstract class BaseTest : IClassFixture<WebDriverFixture>, IDisposable
	{
		protected readonly IWebDriver Driver;
		protected readonly ILog Logger;
		protected readonly ITestOutputHelper Output;

		protected BaseTest(WebDriverFixture fixture, ITestOutputHelper output)
		{
			Driver = fixture.Driver;
			Output = output;

			Log4NetConfig.Configure();
			Logger = Log4NetConfig.GetLogger(GetType());

			Logger.Info($"Test started");
		}

		public void Dispose()
		{
			Logger.Info("Test finished");
			GC.SuppressFinalize(this);
		}
	}
}
