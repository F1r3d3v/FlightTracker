using ProjOb.IO;

namespace ProjOb
{
    public static class ObjectFactory
    {
        public static List<IGrouping<String?, Object>> Deserialize(String filepath)
        {
            ILoader loader = LoaderFactory.CreateLoader(filepath);
            return loader.Load().GroupBy(x => x.ToString()).ToList();
        }
    }
}