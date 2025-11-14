using Core.Config;
using OpenQA.Selenium;

namespace Pages
{
	public class LoginPage : BasePage
	{
		private readonly By UsernameField = By.XPath("//input[@id='user-name']");
		private readonly By PasswordField = By.XPath("//input[@id='password']");
		private readonly By LoginButton = By.XPath("//input[@id='login-button']");
		private readonly By ErrorMessage = By.XPath("//*[@data-test='error']");

		public LoginPage(IWebDriver driver)
			: base(driver)
		{
			Logger.Info("LoginPage initialized");
		}

		#region Navigation

		public LoginPage Open()
		{
			GoToUrl(TestConfig.BaseUrl);
			return this;
		}

		#endregion

		#region Actions

		public LoginPage EnterUsername(string username)
		{
			Type(UsernameField, username);
			return this;
		}

		public LoginPage EnterPassword(string password)
		{
			Type(PasswordField, password);
			return this;
		}

		public LoginPage ClearUsername()
		{
			ClearField(UsernameField);
			return this;
		}

		public LoginPage ClearPassword()
		{
			ClearField(PasswordField);
			return this;
		}

		public LoginPage ClickLogin()
		{
			Click(LoginButton);
			return this;
		}

		public DashboardPage LoginAs(string username, string password)
		{


			EnterUsername(username)
				.EnterPassword(password)
				.ClickLogin();

			return new DashboardPage(Driver);
		}

		#endregion

		#region Verifications

		public bool IsErrorMessageDisplayed()
		{
			return IsElementDisplayed(ErrorMessage);
		}

		public string GetErrorMessageText()
		{
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
