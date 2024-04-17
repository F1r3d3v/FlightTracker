using ProjOb.UI;
using ProjOb.IO;
using static ProjOb.Constants;

namespace ProjOb
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Database db = new Database();
            Logger.Providers = new()
            {
                new FileLogProvider(LogPath)
            };
            Logger.Info("---------- App started ----------");
            TUI.Run(db);
        }
    }
}
