using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Text;

namespace Core.Logging
{
	public static class Log4NetConfig
	{
		private static bool _isConfigured;

		public static ILog GetLogger(Type type)
		{
			Configure();
			return LogManager.GetLogger(type);
		}

		public static void Configure()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			if (_isConfigured)
				return;

			var hierarchy = (Hierarchy)LogManager.GetRepository();

			var layout = new PatternLayout
			{
				ConversionPattern = "[%date{HH:mm:ss}] [%level] %message%newline"
			};
			layout.ActivateOptions();

			var consoleAppender = new ColoredConsoleAppender
			{
				Layout = layout
			};

			consoleAppender.ActivateOptions();

			hierarchy.Root.AddAppender(consoleAppender);
			hierarchy.Root.Level = log4net.Core.Level.Debug;
			hierarchy.Configured = true;

			_isConfigured = true;
		}
	}
}