using FluentAssertions;
using Pages;
using System.Diagnostics.Metrics;
using Tests.Fixtures;
using Tests.TestData;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace Tests.Tests
{
	public class LoginTests : BaseTest
	{
		private readonly LoginPage _loginPage;

		public LoginTests(WebDriverFixture fixture, ITestOutputHelper output)
			: base(fixture, output)
		{
			_loginPage = new LoginPage(Driver);
		}

		/// <summary>
		/// UC-1: Test Login form with empty credentials<br/>
		/// 1. Type any credentials into "Username" and "Password" fields<br/>
		/// 2. Clear the inputs<br/>
		/// 3. Hit the "Login" button<br/>
		/// 4. Check the error messages: "Epic sadface: Username is required"
		/// </summary>
		[Theory]
		[MemberData(nameof(LoginTestData.GetInvalidUsers), MemberType = typeof(LoginTestData))]
		public void UC1_LoginWithEmptyCredentials_ShouldShowUsernameRequiredError(string username, string password)
		{
			// Arrange
			_loginPage.Open();

			//Act
			_loginPage.EnterUsername(username).EnterPassword(password);
			_loginPage.ClearUsername().ClearPassword();
			_loginPage.ClickLogin();

			// Assert
			_loginPage.IsAt().Should().BeTrue("Page should be LoginPage after clicking login with empty fields");
			_loginPage.IsErrorMessageDisplayed().Should().BeTrue("Error message should be visible after clicking login with empty fields");
			var errorMessage = _loginPage.GetErrorMessageText();
			errorMessage.Should().Contain("Epic sadface: Username is required",
				"Error should indicate that username is required");

			Logger.Info("UC-1 Test PASSED: Username required error displayed correctly");
		}

		/// <summary>
		/// UC-2: Test Login form with credentials by passing Username<br/>
		/// 1. Type any credentials in username<br/>
		/// 2. Enter password<br/>
		/// 3. Clear the "Password" input<br/>
		/// 4. Hit the "Login" button<br/>
		/// 5. Check the error messages: "Epic sadface: Password is required"
		/// </summary>
		[Theory]
		[MemberData(nameof(LoginTestData.GetInvalidUsers), MemberType = typeof(LoginTestData))]
		public void UC2_LoginWithOnlyUsername_ShouldShowPasswordRequiredError(string username, string password)
		{
			// Arrange
			_loginPage.Open();

			//Act
			_loginPage.EnterUsername(username).EnterPassword(password);
			_loginPage.ClearPassword();
			_loginPage.ClickLogin();

			// Assert
			_loginPage.IsAt().Should().BeTrue("Page should be LoginPage after clicking login with empty fields");
			_loginPage.IsErrorMessageDisplayed().Should().BeTrue("Error message should be visible after clicking login with empty fields");
			var errorMessage = _loginPage.GetErrorMessageText();
			errorMessage.Should().Contain("Epic sadface: Password is required",
				"Error should indicate that password is required");

			Logger.Info("UC-2 Test PASSED: Password required error displayed correctly");
		}

		/// <summary>
		/// UC-3: Test Login form with credentials by passing Username and Password from <see cref="LoginTestData"/> <br/>
		/// 1. Type credentials in username which are under Accepted username section<br/>
		/// 2. Enter password as secret_sauce<br/>
		/// 3. Click on Login and validate the title "Swag Labs" in the dashboard
		/// </summary>
		[Theory]
		[MemberData(nameof(LoginTestData.GetValidUsers), MemberType = typeof(LoginTestData))]
		public void UC3_LoginWithValidCredentials_ShouldRedirectToDashboard(string username, string password)
		{
			// Arrange
			_loginPage.Open();

			//Act
			var dashboardPage = _loginPage.LoginAs(username, password);

			// Assert
			dashboardPage.IsAt().Should().BeTrue("User should be redirected to dashboard page after successful login");
			var actualTitle = dashboardPage.GetTitle();
			actualTitle.Should().Contain("Swag Labs",
				"Page title should be 'Swag Labs' after successful login");

			Logger.Info("UC-3 Test PASSED: Successfully logged in and verified dashboard title");
		}
	}
}
