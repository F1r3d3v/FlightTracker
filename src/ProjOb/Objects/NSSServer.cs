using NetworkSourceSimulator;
using ProjOb.IO;

namespace ProjOb
{
    static class NSSServer
    {
        public static void RunServer(String filepath, Database db)
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;


            ILoader loader = new NSSLoader(filepath);
            Task.Run(() => loader.LoadToDatabase(db), cts.Token);

            String? selection;

            do
            {
                Console.Write("1.Print\n2.Exit\nSelect: ");
                selection = Console.ReadLine()!.ToLower();

                if (selection == "print" || selection == "1")
                {
                    TimeSpan time = DateTime.Now.TimeOfDay;
                    String output = String.Format($"snapshot_{time.ToString("hh_mm_ss")}.json");
                    lock(db)
                    {
                        db.Serialize(output);
                    }
                }
            } while (selection == "exit" || selection == "2");

            cts.Cancel();
        }
    }
}
