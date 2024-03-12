namespace ProjOb
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Database db = new Database();
            NSSServer.RunServer("example_data.ftr", db, 15, 30);
        }
    }
}
