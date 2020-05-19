using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using log4net;
using log4net.Core;
using log4net.Layout;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using static log4net.Appender.ColoredConsoleAppender;

namespace KO.TBLCryptoEditor.Utils
{
    public class LoggerConfiguration
    {
        public string LogPatternLayout { get; }
        public bool EnableRollingFileAppender { get; }
        public bool EnableColoredConsoleAppender { get; }
        public bool EnableMemoryAppender { get; }

        public LoggerConfiguration(string logPatternLayout = null,
                                   bool enableRollingFileAppender = true, 
                                   bool enableColoredConsoleAppender = true, 
                                   bool enableMemoryAppender = true)
        {
            LogPatternLayout = logPatternLayout;
            EnableRollingFileAppender = enableRollingFileAppender;
            EnableColoredConsoleAppender = enableColoredConsoleAppender;
            EnableMemoryAppender = enableMemoryAppender;
        }
    }

    public static class Logger
    {
        public static ILog GetLogger([CallerFilePath] string filename = "")
        {
            string loggerName = Path.GetFileNameWithoutExtension(filename);
            return LogManager.GetLogger(loggerName);
        }

        public static void Initialize(LoggerConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = new LoggerConfiguration();

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            DynamicPatternLayout pattern = new DynamicPatternLayout();
            pattern.Header = "%newline" +
                             "%date Application started..." +
                             "%newline";

            if (String.IsNullOrEmpty(configuration.LogPatternLayout))
                pattern.ConversionPattern = "%date %-5level [%logger::%M]: %message%newline";
                //patternLayout.ConversionPattern = "[%level] [%M] [%class]%newline[%date]: %message%newline%exception";
                //patternLayout.ConversionPattern = "%newline[%level] [%M] [%class]%newline[%date]: %message%newline%exception";
            else
                pattern.ConversionPattern = configuration.LogPatternLayout;

            pattern.Footer = "%date Application ended..." +
                             "%newline";

            pattern.ActivateOptions();

            if (configuration.EnableRollingFileAppender)
            {
                RollingFileAppender fileAppender = CreateRollingFileAppender(pattern);
                hierarchy.Root.AddAppender(fileAppender);
            }

            if (configuration.EnableColoredConsoleAppender)
            {
                ColoredConsoleAppender consoleAppender = CreateConsoleAppender(pattern);
                hierarchy.Root.AddAppender(consoleAppender);
            }

            if (configuration.EnableMemoryAppender)
            {
                MemoryAppender memory = new MemoryAppender();
                memory.ActivateOptions();
                hierarchy.Root.AddAppender(memory);
            }

            hierarchy.Configured = true;
#if DEBUG
            hierarchy.Root.Level = Level.Debug;
#else
            hierarchy.Root.Level = Level.Info;
#endif
        }

        private static RollingFileAppender CreateRollingFileAppender(PatternLayout patternLayout)
        {
            RollingFileAppender fileAppender = new RollingFileAppender();
            fileAppender.File = $"{Assembly.GetExecutingAssembly().GetName().Name}.log";
            fileAppender.LockingModel = new FileAppender.InterProcessLock();
            fileAppender.Encoding = Encoding.UTF8;
            fileAppender.AppendToFile = true;
            fileAppender.RollingStyle = RollingFileAppender.RollingMode.Size;
            fileAppender.MaximumFileSize = "50MB";
            fileAppender.MaxSizeRollBackups = 10;
            fileAppender.StaticLogFileName = true;
            fileAppender.Layout = patternLayout;
            fileAppender.ActivateOptions();

            return fileAppender;
        }

        private static ColoredConsoleAppender CreateConsoleAppender(PatternLayout patternLayout)
        {
            ColoredConsoleAppender consoleAppender = new ColoredConsoleAppender();
            consoleAppender.Layout = patternLayout;
            var colorMapping = new List<LevelColors>
            {
                new LevelColors
                {
                    Level = Level.Debug,
                    ForeColor = Colors.Green
                },
                new LevelColors
                {
                    Level = Level.Info,
                    ForeColor = Colors.White
                },
                new LevelColors
                {
                    Level = Level.Warn,
                    ForeColor = Colors.Yellow
                },
                new LevelColors
                {
                    Level = Level.Error,
                    ForeColor = Colors.Red
                },
                new LevelColors
                {
                    Level = Level.Fatal,
                    ForeColor = Colors.Red | Colors.HighIntensity
                }
            };
            colorMapping.ForEach(levelColor => consoleAppender.AddMapping(levelColor));
            consoleAppender.ActivateOptions();

            return consoleAppender;
        }
    }
}
