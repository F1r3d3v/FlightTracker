using ProjOb.UI;

namespace ProjOb
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Database db = new Database();
            TUI.Run(db);
        }
    }
}
