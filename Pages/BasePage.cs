using Core.Logging;
using log4net;
using OpenQA.Selenium;

namespace Pages
{
	public abstract class BasePage
	{
		protected readonly IWebDriver Driver;
		protected readonly ILog Logger;

		protected BasePage(IWebDriver driver)
		{
			Driver = driver;
			Logger = Log4NetConfig.GetLogger(GetType());
		}

		#region Navigation

		public string GetTitle()
		{
			return Driver.Title;
		}

		public string GetCurrentUrl()
		{
			return Driver.Url;
		}

		public void GoToUrl(string url)
		{
			string logMessage = $"Opening page: {url}";
			Logger.Info(logMessage);
			Driver.Navigate().GoToUrl(url);
		}

		public void Refresh()
		{
			Logger.Debug("Refreshing page");
			Driver.Navigate().Refresh();
		}

		#endregion

		#region Element Finding

		protected IWebElement Find(By locator)
		{
			try
			{
				string logMessage = $"Searching for element: {locator}";
				Logger.Debug(logMessage);
				return Driver.FindElement(locator);
			}
			catch (NoSuchElementException ex)
			{
				string errorMessage = $"Element not found: {locator}";
				Logger.Error(errorMessage, ex);
				throw;
			}
		}

		#endregion

		#region Actions

		protected void Click(By locator)
		{
			string logMessage = $"Clicking element: {locator}";
			Logger.Info(logMessage);
			var element = Find(locator);
			element.Click();
		}

		protected void Type(By locator, string text)
		{
			string logMessage = $"Typing '{text}' into element: {locator}";
			Logger.Info(logMessage);
			var element = Find(locator);
			element.Clear();
			element.SendKeys(text);
		}

		protected string GetText(By locator)
		{
			string logMessage = $"Getting text from element: {locator}";
			Logger.Debug(logMessage);
			return Find(locator).Text;
		}

		protected bool IsElementDisplayed(By locator)
		{
			return Find(locator).Displayed;
		}

		#endregion
	}
}
