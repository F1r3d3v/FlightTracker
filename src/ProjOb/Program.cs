using ProjOb.IO;

namespace ProjOb
{
    internal class Program
    {
        public static void Serialize(object[] objs, String filepath)
        {
            IWriter wr = WriterFactory.Create(filepath) ?? throw new Exception("Can't create a file writer.");

            wr.Write(objs);
            wr.Close();
        }

        static void Main(String[] args)
        {
            String inputFilepath = (args.Length == 0) ? "example_data.ftr" : args[0];
            String outputFilename = $"{Path.GetFileNameWithoutExtension(inputFilepath)}.json";

            Console.WriteLine("Loading ftr file!");
            var objList = ObjectFactory.Deserialize(inputFilepath);

            Console.WriteLine("Serializing objects collection to json!");
            Serialize(objList.ToArray(), outputFilename);

            Console.WriteLine("Goodbye!");
        }
    }
}
