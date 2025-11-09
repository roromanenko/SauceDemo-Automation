namespace Tests.TestData
{
	public static class LoginTestData
	{
		public static IEnumerable<object[]> GetValidUsers()
		{
			yield return new object[] { "standard_user", "secret_sauce" };
			yield return new object[] { "locked_out_user", "secret_sauce" };
			yield return new object[] { "problem_user", "secret_sauce" };
			yield return new object[] { "performance_glitch_user", "secret_sauce" };
			yield return new object[] { "error_user", "secret_sauce" };
			yield return new object[] { "visual_user", "secret_sauce" };
		}

		public static IEnumerable<object[]> GetInvalidUsers()
		{
			yield return new object[] { "admin", "admin_password" };
			yield return new object[] { "JohnDoe", "secret_password" };
		}
	}
}
