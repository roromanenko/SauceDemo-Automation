namespace Tests.TestData
{
	/// <summary>
	/// Provides test data for login-related test scenarios.
	/// </summary>
	public static class LoginTestData
	{
		public static TheoryData<string, string> GetValidUsers()
		{
			return new TheoryData<string, string>
			{
				{ "standard_user", "secret_sauce" },
				{ "problem_user", "secret_sauce" },
				{ "performance_glitch_user", "secret_sauce" },
				{ "error_user", "secret_sauce" },
				{ "visual_user", "secret_sauce" }
			};
		}

		public static TheoryData<string, string> GetInvalidUsers()
		{
			return new TheoryData<string, string>
			{
				{ "admin", "admin_password" },
				{ "JohnDoe", "secret_password" }
			};
		}

		public static TheoryData<string, string> GetLockedOutUsers()
		{
			return new TheoryData<string, string>
			{
				{ "locked_out_user", "secret_sauce" }
			};
		}
	}
}
