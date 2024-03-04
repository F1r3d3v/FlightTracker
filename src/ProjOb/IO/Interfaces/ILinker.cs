namespace ProjOb.IO
{
    internal interface ILinker
    {
        void Link(Dictionary<String, String[]> records, List<Object> objects);
    }
}