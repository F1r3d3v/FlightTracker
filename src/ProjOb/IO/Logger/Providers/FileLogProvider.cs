namespace ProjOb.IO
{
    public class FileLogProvider : ILogProvider
    {
        private readonly String _basePath;
        public bool Enabled { get; set; } = true;

        public FileLogProvider(String basePath)
        {
            _basePath = basePath;
            Directory.CreateDirectory(basePath);
        }

        public void Log(LogLevel logLevel, string message) => Log(new LogMessage(logLevel, message, DateTime.Now));

        public void Log(LogMessage message)
        {
            string filePath = Path.Combine(_basePath, $"{DateTime.Now:yyyy-MM-dd}.log");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(message);
            }
        }
    }
}
