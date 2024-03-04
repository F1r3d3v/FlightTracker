using ProjOb.IO;

namespace ProjOb
{
    public static class ObjectFactory
    {
        public static void Serialize(Database database, String filepath)
        {
            IWriter wr = WriterFactory.Create(filepath) ?? throw new Exception("Can't create a file writer.");

            wr.Write(database);
            wr.Close();
        }
        public static Database Deserialize(String filepath)
        {
            ILoader loader = LoaderFactory.CreateLoader(filepath);
            loader.LoadToDatabase(out Database database);
            return database;
        }
    }
}