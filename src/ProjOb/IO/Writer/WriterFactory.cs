namespace ProjOb.IO
{
    public static class WriterFactory
    {
        private static readonly Dictionary<string, Func<String, IWriter>> factories = new()
        {
            { ".JSON", (String path) => new JSONWriter(path) },
        };

        public static IWriter? Create(string filepath)
        {
            string ext = Path.GetExtension(filepath).ToUpperInvariant();

            if (factories.TryGetValue(ext, out var obj))
            {
                return obj(filepath);
            }
            else
            {
                Console.WriteLine($"Invalid extension: {ext}");
                return null;
            }
        }
    }
}