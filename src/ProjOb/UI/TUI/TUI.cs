using ProjOb.IO;
using ProjOb.Media;

namespace ProjOb.UI
{
    internal static class TUI
    {
        public static void Run(Database db)
        {
            bool finishing = true;

            var reportDel = (Database db) =>
            {
                List<IMedia> mediaList = [new Television("Telewizja Abelowa"),
                                          new Television("Kanał TV-tensor"),
                                          new Radio("Radio Kwantyfikator"),
                                          new Radio("Radio Shmem"),
                                          new Newspaper("Gazeta Kategoryczna"),
                                          new Newspaper("Dziennik Politechniczny")];

                List<IReportable> objs = [.. db.Airports.Values, .. db.CargoPlanes.Values, .. db.PassengerPlanes.Values];
                NewsGenerator news = new NewsGenerator(mediaList, objs);

                string? content;
                while ((content = news.GenerateNextNews()) != null)
                {
                    Console.WriteLine(content);
                }

                Console.WriteLine("\nPress Any key to continue...");
                Console.ReadKey();
                finishing = false;
            };

            var snapshotDel = (Database db) =>
            {
                lock (db)
                {
                    db.Serialize($"snapshot_{DateTime.Now:HH_mm_ss}.json");
                }
                finishing = false;
            };

            string[] src = ["Local Database", "Network Stream"];
            int sel = SelectionMenu.CreateSelectionMenu(src, "Choose Source:");

            ILoader? loader = null;
            switch (src[sel])
            {
                case "Local Database":
                    loader = new FTRLoader("example_data.ftr");
                    break;

                case "Network Stream":
                    loader = new NSSLoader("example_data.ftr", 10, 15);
                    break;
            }
            Task.Run(() => loader?.LoadToDatabase(db));

            var opts = new Dictionary<string, Action>()
            {
                { "Flight Tracker", () => { FlightTracker.RunGUI(db); finishing = false; }  },
                { "Report", () => reportDel(db) },
                { "Snapshot", () => snapshotDel(db) },
                { "Exit", () => Environment.Exit(0) },
            };

            do
            {
                finishing = true;
                SelectionMenu.CreateSelectionMenu(opts, "Main Menu:");
            }
            while (!finishing);
        }
    }
}
