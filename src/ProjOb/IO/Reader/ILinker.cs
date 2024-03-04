namespace ProjOb.IO
{
    internal interface ILinker
    {
        void Link(Dictionary<String, String[]> records, Database database);
    }
}