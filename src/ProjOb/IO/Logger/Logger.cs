namespace ProjOb.IO
{
    public static class Logger
    {
        public static LoggerWorker Worker { get; private set; } = new LoggerWorker();
        public static List<ILogProvider> Providers { get; set; } = [];

        private static bool _enabled;
        public static bool Enabled
        {
            get => _enabled;
            set
            {
                if (value)
                    Worker.Start();
                else
                    Worker.Stop();

                _enabled = value;
            }
        }

        // Async Methods

        public static void InfoAsync(String message) => LogAsync(LogLevel.Info, message);
        public static void WarningAsync(String message) => LogAsync(LogLevel.Warning, message);
        public static void ErrorAsync(String message) => LogAsync(LogLevel.Error, message);
        public static void DebugAsync(String message) => LogAsync(LogLevel.Debug, message);

        public static void LogAsync(LogLevel logLevel, String message)
            => LogAsync(new LogMessage(logLevel, message, DateTime.Now));

        public static void LogAsync(LogLevel logLevel, String message, ILogProvider provider)
            => LogAsync(new LogMessage(logLevel, message, DateTime.Now), provider);

        public static void LogAsync(LogMessage message, ILogProvider provider)
            => Worker.ProcessAsync(message, provider);

        public static void LogAsync(LogMessage message)
        {
            foreach (ILogProvider provider in Providers)
            {
                Worker.ProcessAsync(message, provider);
            }
        }

        // Sync Methods

        public static void Info(String message) => Log(LogLevel.Info, message);
        public static void Warning(String message) => Log(LogLevel.Warning, message);
        public static void Error(String message) => Log(LogLevel.Error, message);
        public static void Debug(String message) => Log(LogLevel.Debug, message);

        public static void Log(LogLevel logLevel, String message)
            => Log(new LogMessage(logLevel, message, DateTime.Now));

        public static void Log(LogLevel logLevel, String message, ILogProvider provider)
            => Log(new LogMessage(logLevel, message, DateTime.Now), provider);

        public static void Log(LogMessage message, ILogProvider provider)
            => Worker.Process(message, provider);

        public static void Log(LogMessage message)
        {
            foreach (ILogProvider provider in Providers)
            {
                Worker.Process(message, provider);
            }
        }
    }
}
