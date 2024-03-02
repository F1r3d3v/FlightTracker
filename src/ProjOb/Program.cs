using ProjOb.IO;

namespace ProjOb
{
    internal class Program
    {
        static void Main(String[] args)
        {
            List<Object> objList = [];
            String inputFilepath = (args.Length == 0) ? "example_data.ftr" : args[0];
            String outputFilename = Path.GetFileNameWithoutExtension(inputFilepath);

            Console.WriteLine("Loading ftr file!");
            IReader rd = ReaderFactory.Create(inputFilepath) ?? throw new Exception("Can't create a file reader.");

            String[]? s;
            while ((s = rd.Read()) != null)
            {
                Object? obj = ObjectFactory.Create(s);
                if (obj != null)
                    objList.Add(obj);
            }
            rd.Close();

            Console.WriteLine("Serializing objects collection to json!");
            IWriter wr = WriterFactory.Create($"{outputFilename}.json") ?? throw new Exception("Can't create a file writer.");

            wr.Write([.. objList]);
            wr.Close();

            Console.WriteLine("Goodbye!");
        }
    }
}
