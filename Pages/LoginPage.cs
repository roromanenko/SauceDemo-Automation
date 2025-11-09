using Core.Config;
using OpenQA.Selenium;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pages
{
	public class LoginPage : BasePage
	{
		private readonly By UsernameField = By.Id("user-name");
		private readonly By PasswordField = By.Id("password");
		private readonly By LoginButton = By.Id("login-button");
		private readonly By ErrorMessage = By.CssSelector("[data-test='error']");

		public LoginPage(IWebDriver driver)
			: base(driver)
		{
			Logger.Info("LoginPage initialized");
		}

		#region Navigation

		public LoginPage Open()
		{
			string url = TestConfig.BaseUrl;
			GoToUrl(url);
			return this;
		}

		#endregion

		#region Actions

		public LoginPage EnterUsername(string username)
		{
			string logMessage = $"Entering username: {username}";
			Logger.Info(logMessage);
			Type(UsernameField, username);
			return this;
		}

		public LoginPage EnterPassword(string password)
		{
			string logMessage = $"Entering password: {password}";
			Logger.Info(logMessage);
			Type(PasswordField, password);
			return this;
		}

		public LoginPage ClearUsername()
		{
			Logger.Info("Clearing username field");
			Find(UsernameField).Clear();
			return this;
		}

		public LoginPage ClearPassword()
		{
			Logger.Info("Clearing password field");
			Find(PasswordField).Clear();
			return this;
		}

		public LoginPage ClickLogin()
		{
			Logger.Info("Clicking login button");
			Click(LoginButton);
			return this;
		}

		public DashboardPage LoginAs(string username, string password)
		{
			string logMessage = $"Logging in as: {username}";
			Logger.Info(logMessage);
			EnterUsername(username);
			EnterPassword(password);
			ClickLogin();

			return new DashboardPage(Driver);
		}

		public LoginPage LoginWithInvalidCredentials(string username, string password)
		{
			Logger.Info("Attempting login with invalid credentials");
			EnterUsername(username);
			EnterPassword(password);
			ClickLogin();

			return this;
		}

		#endregion

		#region Verifications

		public bool IsErrorMessageDisplayed()
		{
			return IsElementDisplayed(ErrorMessage);
		}

		public string GetErrorMessageText()
		{
			Logger.Debug("Getting error message text");
			return GetText(ErrorMessage);
		}

		public bool IsAt()
		{
			Logger.Debug("Verifying we are on Login page");
			return IsElementDisplayed(UsernameField)
				&& IsElementDisplayed(PasswordField)
				&& IsElementDisplayed(LoginButton);
		}

		#endregion
	}
}
