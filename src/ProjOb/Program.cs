using System.Collections.Generic;

namespace ProjOb
{
    internal class Program
    {
        static void Main(String[] args)
        {
            String inputFilepath = (args.Length == 0) ? "example_data.ftr" : args[0];
            String outputFilename = $"{Path.GetFileNameWithoutExtension(inputFilepath)}.json";

            Console.WriteLine("Loading ftr file!");
            Database database = ObjectFactory.Deserialize(inputFilepath);

            Console.WriteLine("Serializing objects collection to json!");
            ObjectFactory.Serialize(database, outputFilename);

            Console.WriteLine("Goodbye!");
        }
    }
}
