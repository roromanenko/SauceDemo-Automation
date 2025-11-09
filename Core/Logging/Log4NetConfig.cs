using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;

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

			consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
			{
				Level = log4net.Core.Level.Debug,
				ForeColor = ColoredConsoleAppender.Colors.Green
			});
			consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
			{
				Level = log4net.Core.Level.Info,
				ForeColor = ColoredConsoleAppender.Colors.White
			});
			consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
			{
				Level = log4net.Core.Level.Warn,
				ForeColor = ColoredConsoleAppender.Colors.Yellow | ColoredConsoleAppender.Colors.HighIntensity
			});
			consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
			{
				Level = log4net.Core.Level.Error,
				ForeColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity
			});
			consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
			{
				Level = log4net.Core.Level.Fatal,
				ForeColor = ColoredConsoleAppender.Colors.White,
				BackColor = ColoredConsoleAppender.Colors.Red
			});

			consoleAppender.ActivateOptions();

			hierarchy.Root.AddAppender(consoleAppender);
			hierarchy.Root.Level = log4net.Core.Level.Debug;
			hierarchy.Configured = true;

			_isConfigured = true;
		}
	}
}