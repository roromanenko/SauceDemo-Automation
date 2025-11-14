using OpenQA.Selenium;

namespace Pages
{
	public class DashboardPage : BasePage
	{
		private readonly By Title = By.XPath("//div[@class='app_logo']");
		private readonly By InventoryContainer = By.XPath("//div[@id='inventory_container']");
		private readonly By BurgerMenu = By.XPath("//div[@class='bm-burger-button']");
		private readonly By LogoutLink = By.XPath("//a[@id='logout_sidebar_link']");

		public DashboardPage(IWebDriver driver)
			: base(driver)
		{
			Logger.Info("DashboardPage initialized");
		}

		#region Verifications

		public string GetHeaderText()
		{
			return GetText(Title);
		}

		public bool IsAt()
		{
			return IsElementDisplayed(InventoryContainer)
				&& IsElementDisplayed(BurgerMenu);
		}

		#endregion

		#region Actions

		public DashboardPage OpenBurgerMenu()
		{
			Click(BurgerMenu);
			return this;
		}

		public LoginPage Logout()
		{
			OpenBurgerMenu();
			Click(LogoutLink);
			return new LoginPage(Driver);
		}

		#endregion
	}
}
