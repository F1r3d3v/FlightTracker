using ProjOb.IO;
using ProjOb.GUI;

namespace ProjOb
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Database db = new Database();
            //ILoader loader = new NSSLoader("example_data.ftr", 5, 10);
            ILoader loader = new FTRLoader("example_data.ftr");
            FlightTracker.RunGUI(loader, db);
        }
    }
}
