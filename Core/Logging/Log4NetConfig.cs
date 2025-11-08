using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging
{
	public static class Log4NetConfig
	{
		private static bool _isConfigured;

		public static void Configure()
		{
			if (_isConfigured)
				return;

			var hierarchy = (Hierarchy)LogManager.GetRepository();

			var patternLayout = new PatternLayout
			{
				ConversionPattern = "[%date{HH:mm:ss}] [%level] %message%newline"
			};
			patternLayout.ActivateOptions();

			var consoleAppender = new ConsoleAppender
			{
				Layout = patternLayout
			};
			consoleAppender.ActivateOptions();

			hierarchy.Root.AddAppender(consoleAppender);
			hierarchy.Root.Level = log4net.Core.Level.Debug;
			hierarchy.Configured = true;

			_isConfigured = true;
		}

		public static ILog GetLogger(Type type)
		{
			Configure();
			return LogManager.GetLogger(type);
		}
	}
}
