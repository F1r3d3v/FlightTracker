namespace ProjOb.IO
{
    internal static class LoaderFactory
    {
        private static readonly Dictionary<String, Func<String, ILoader>> factories = new()
        {
            { ".FTR", (String filepath) => new FTRLoader(filepath) },
        };

        public static ILoader CreateLoader(String filepath)
        {
            string ext = Path.GetExtension(filepath).ToUpperInvariant();

            if (factories.TryGetValue(ext, out var obj))
            {
                return obj(filepath);
            }
            else
            {
                throw new ArgumentException($"Invalid extension: {ext}");
            }
        }
    }
}
