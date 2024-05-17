using ProjOb.IO;
using ProjOb.UI;
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
                {
                    Enabled = true,
                }
            };
            Logger.InfoAsync("---------- App started ----------");
            TUI.Run(db);
        }
    }
}
