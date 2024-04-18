namespace ProjOb.IO
{
    public class ConsoleLogProvider : ILogProvider
    {
        public Dictionary<LogLevel, ConsoleColor> ColorMap = new()
        {
            [LogLevel.Info] = ConsoleColor.White,
            [LogLevel.Warning] = ConsoleColor.Yellow,
            [LogLevel.Error] = ConsoleColor.Red,
            [LogLevel.Debug] = ConsoleColor.Green
        };

        public void Log(LogLevel logLevel, string message) => Log(new LogMessage(logLevel, message, DateTime.Now));

        public void Log(LogMessage message)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ColorMap[message.LogLevel];
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
    }
}
