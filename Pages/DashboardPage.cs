using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages
{
	public class DashboardPage : BasePage
	{
		private readonly By Title = By.ClassName("app-logo");
		private readonly By InventoryContainer = By.Id("inventory_container");
		private readonly By BurgerMenu = By.ClassName("bm-burger-button");
		private readonly By LogoutLink = By.Id("logout_sidebar_link");

		public DashboardPage(IWebDriver driver)
			: base(driver)
		{
			Logger.Info("DashboardPage initialized");
		}

		#region Verifications

		public string GetHeaderText()
		{
			Logger.Debug("Getting dashboard header text");
			return GetText(Title);
		}

		public bool IsAt()
		{
			Logger.Debug("Verifying we are on Dashboard page");
			return IsElementDisplayed(InventoryContainer)
				&& IsElementDisplayed(BurgerMenu)
				&& GetTitle() == "Swag Labs";
		}

		#endregion

		#region Actions

		public DashboardPage OpenBurgerMenu()
		{
			Logger.Info("Opening burger menu");
			Click(BurgerMenu);
			return this;
		}

		public LoginPage Logout()
		{
			Logger.Info("Logging out");
			Click(LogoutLink);
			return new LoginPage(Driver);
		}

		#endregion
	}
}
