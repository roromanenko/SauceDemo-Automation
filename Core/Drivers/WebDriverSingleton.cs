using Core.Config;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Drivers
{
	public sealed class WebDriverSingleton
	{
		private static readonly AsyncLocal<IWebDriver?> _driver = new();

		private WebDriverSingleton() { }

		public static IWebDriver Driver
		{
			get
			{
				if (_driver.Value is null)
				{
					_driver.Value = DriverFactory.CreateDriver(TestConfig.Browser);
					_driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestConfig.ImplicitWaitSeconds);
				}
				return _driver.Value;
			}
		}

		public static void QuitDriver()
		{
			_driver.Value?.Quit();
			_driver.Value?.Dispose();
			_driver.Value = null;
		}
	}
}
