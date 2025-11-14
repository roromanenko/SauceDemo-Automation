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
			Driver.Navigate().GoToUrl(url);
		}

		public void Refresh()
		{
			Driver.Navigate().Refresh();
		}

		#endregion

		#region Element Finding

		protected IWebElement Find(By locator)
		{
			try
			{
				return Driver.FindElement(locator);
			}
			catch (NoSuchElementException ex)
			{
				string errorMessage = $"Element not found: {locator}";
				Logger.Error(errorMessage, ex);
				throw;
			}
		}

		protected IReadOnlyCollection<IWebElement> FindAll(By locator)
		{
			try
			{
				return Driver.FindElements(locator);
			}
			catch (NoSuchElementException ex)
			{
				string errorMessage = $"Elements not found: {locator}";
				Logger.Error(errorMessage, ex);
				throw;
			}
		}

		#endregion

		#region Actions

		protected void Click(By locator)
		{
			var element = Find(locator);
			element.Click();
		}

		protected void Type(By locator, string text)
		{
			var element = Find(locator);
			element.Clear();
			element.SendKeys(text);
		}

		protected void ClearField(By locator)
		{
			var element = Find(locator);
			element.SendKeys(Keys.Control + "a");
			element.SendKeys(Keys.Delete);
		}

		#endregion

		#region Element State

		protected string GetText(By locator)
		{
			return Find(locator).Text;
		}

		protected bool IsElementDisplayed(By locator)
		{
			try
			{
				return Find(locator).Displayed;
			}
			catch (NoSuchElementException)
			{
				return false;
			}
		}

		#endregion
	}
}
