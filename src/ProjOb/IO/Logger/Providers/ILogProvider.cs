namespace ProjOb.IO
{
    public interface ILogProvider
    {
        bool Enabled { get; set; }
        void Log(LogMessage message);
    }
}
