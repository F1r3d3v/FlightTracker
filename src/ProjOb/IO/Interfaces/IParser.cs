namespace ProjOb.IO
{
    internal interface IParser
    {
        void Parse(Dictionary<String, String[]> records, List<Object> objects);
    }
}
