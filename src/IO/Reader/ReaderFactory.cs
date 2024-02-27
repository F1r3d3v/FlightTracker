namespace ProjOb.IO
{
    public static class ReaderFactory
    {
        private static readonly Dictionary<string, Func<String, IReader>> factories = new()
        {
            { ".FTR", (String path) => new FTRReader(path) },
        };

        public static IReader? Create(string filepath)
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