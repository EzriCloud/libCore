using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Config;

namespace libCore.Logging
{
    public sealed class Logger
    {
        private readonly ILog _log;

        public static void Initialize()
        {
            var mainConfigFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            var myDirectory = Path.GetDirectoryName(mainConfigFile) ?? string.Empty;

            var log4NetConfig = Path.Combine(myDirectory, "log4net.config");

            if (File.Exists(log4NetConfig))
                XmlConfigurator.Configure(new FileInfo(log4NetConfig));
            else
                BasicConfigurator.Configure();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Logger For<T>()
        {
            return new Logger(LogManager.GetLogger(Assembly.GetCallingAssembly(), typeof(T)));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Logger For<T>(T source)
        {
            return new Logger(LogManager.GetLogger(Assembly.GetCallingAssembly(), source.GetType()));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Logger For(Type type)
        {
            return new Logger(LogManager.GetLogger(Assembly.GetCallingAssembly(), type));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Logger For(string loggerName)
        {
            return new Logger(LogManager.GetLogger(Assembly.GetCallingAssembly(), loggerName));
        }

        private Logger(ILog log)
        {
            _log = log;
        }

        [Conditional("DEBUG")]
        public void Debug(string message)
        {
            _log.Debug(message);
        }

        [Conditional("DEBUG")]
        public void Debug(string format, params object[] args)
        {
            _log.Debug(string.Format(format, args));
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Info(string format, params object[] args)
        {
            _log.Info(string.Format(format, args));
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string format, params object[] args)
        {
            _log.Warn(string.Format(format, args));
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(Exception exception)
        {
            _log.Error("ERROR", exception);
        }

        public void Error(string format, params object[] args)
        {
            _log.Error(string.Format(format, args));
        }

        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string format, params object[] args)
        {
            _log.Fatal(string.Format(format, args));
        }

        public void Fatal(string message, Exception exception)
        {
            _log.Fatal(message, exception);
        }
    }
}