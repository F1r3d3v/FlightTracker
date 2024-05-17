namespace ProjOb.IO
{
    public class LogMessage
    {
        public LogLevel LogLevel { get; set; }
        public String Description { get; set; }
        public DateTime Time { get; set; }

        public LogMessage(LogLevel level, String description, DateTime time)
        {
            LogLevel = level;
            Description = description;
            Time = time;
        }

        public override string ToString() => $"{Time:HH:mm:ss} | {String.Format("{0}", $"[{LogLevel}]"),-9} | {Description}";
    }
}
