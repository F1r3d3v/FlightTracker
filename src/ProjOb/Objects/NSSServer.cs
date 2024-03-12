using NetworkSourceSimulator;
using ProjOb.IO;
using System.Globalization;

namespace ProjOb
{
    static class NSSServer
    {
        public static void RunServer(String filepath, Database db, int minDelay = 50, int maxDelay = 100)
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;


            ILoader loader = new NSSLoader(filepath, minDelay, maxDelay);
            Task.Run(() => loader.LoadToDatabase(db), cts.Token);

            String? selection;

            do
            {
                Console.Clear();
                Console.Write("1.Print\n2.Exit\nSelect: ");
                selection = Console.ReadLine()!.ToLower();

                if (selection == "print" || selection == "1")
                {
                    String output = $"snapshot_{DateTime.Now.ToString("HH_mm_ss", CultureInfo.InvariantCulture)}.json";
                    lock(db)
                    {
                        db.Serialize(output);
                    }
                }
            } while (selection != "exit" && selection != "2");

            cts.Cancel();
        }
    }
}
