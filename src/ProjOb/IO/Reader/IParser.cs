namespace ProjOb.IO
{
    internal interface IParser
    {
        List<Object> Parse(Dictionary<String, String[]> records);
    }
}
