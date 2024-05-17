using ProjOb.IO;
using ProjOb.Media;
using ProjOb.Query;
using static ProjOb.Constants;

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
            };

            var showLogs = () =>
            {
                ILogProvider logPr = new ConsoleLogProvider();
                Logger.Worker.Lock.EnterUpgradeableReadLock();
                try
                {
                    using (StreamReader sr = new StreamReader(Path.Combine(LogPath, $"{DateTime.Now:yyyy-MM-dd}.log")))
                    {
                        while (!sr.EndOfStream)
                        {
                            String? line = sr.ReadLine();
                            if (line != null)
                            {
                                String[] mesg = line.Split('|');
                                if (Enum.TryParse((mesg[1].Trim())[1..^1], true, out LogLevel loglevel))
                                {
                                    if (DateTime.TryParse(mesg[0], out DateTime time))
                                    {
                                        ILogProvider consoleLogPr = new ConsoleLogProvider();
                                        LogMessage message = new(loglevel, mesg[2][1..], time);
                                        Logger.Log(message, consoleLogPr);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No logs to display\n");
                    Console.WriteLine("\nPress Any key to continue...");
                    Console.ReadKey();
                    return;
                }
                finally
                {
                    Logger.Worker.Lock.ExitUpgradeableReadLock();
                }

                Console.WriteLine("\nPress Any key to continue...");
                Console.ReadKey();
            };

            var clearLogs = () =>
            {
                DirectoryInfo dInfo = new DirectoryInfo(LogPath);
                foreach (FileInfo fInfo in dInfo.EnumerateFiles())
                {
                    fInfo.Delete();
                }
            };

            var snapshotDel = (Database db) =>
            {
                lock (db)
                {
                    db.Serialize($"snapshot_{DateTime.Now:HH_mm_ss}.json");
                }
            };

            var queryDel = () =>
            {
                Console.WriteLine("Enter Query:");
                TerminalHelper.SetCursorVisibility(true);
                String str = Console.ReadLine() ?? "";
                TerminalHelper.SetCursorVisibility(false);
                try
                {
                    QueryStatement q = new QueryStatement(str, db);
                    q.Execute()?.Display();

                    Console.WriteLine("Query Executed Successfully!");
                }
                catch (Exception e)
                {
                    TerminalHelper.MoveCursorToHome();
                    TerminalHelper.ClearScreen(TerminalHelper.ClearScreenType.FromCurToEnd);
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("\nPress Any key to continue...");
                Console.ReadKey();
            };

            string[] src = ["Local Database", "Network Stream", "Local Database with Network Stream Changes"];
            int sel = SelectionMenu.CreateSelectionMenu(src, "Choose Source:");

            ILoader? loader1 = null;
            ILoader? loader2 = null;
            switch (src[sel])
            {
                case "Local Database":
                    loader1 = new FTRLoader("example_data.ftr");
                    break;

                case "Network Stream":
                    loader1 = new NSSLoader("example_data.ftr", 10, 15);
                    break;

                case "Local Database with Network Stream Changes":
                    loader1 = new FTRLoader("example_data.ftr");
                    loader2 = new NSSLoader("example.ftre", 200, 300);
                    break;
            }
            Task.Run(() =>
            {
                loader1?.LoadToDatabase(db);
                loader2?.LoadToDatabase(db);
            });

            var optsLogs = new Dictionary<string, Action>()
            {
                { "Show Logs", () => showLogs()  },
                { "Clear Logs", () => clearLogs() },
                { "Back", () => { } },
            };

            var opts = new Dictionary<string, Action>()
            {
                { "Flight Tracker", () => { FlightTracker.RunGUI(db); finishing = false; }  },
                { "Query", () => { queryDel(); finishing = false; } },
                { "Report", () => { reportDel(db); finishing = false; } },
                { "Logs", () => { SelectionMenu.CreateSelectionMenu(optsLogs, "Logs"); finishing = false; } },
                { "Snapshot", () => { snapshotDel(db); finishing = false; } },
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
