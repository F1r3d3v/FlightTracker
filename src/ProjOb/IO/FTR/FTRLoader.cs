using ProjOb.Exceptions;

namespace ProjOb.IO
{
    internal class FTRLoader : ILoader
    {
        public IReader reader { get; }

        public IValidator validator { get; }

        public IParser parser { get; }

        public ILinker linker { get; }

        internal FTRLoader(String filepath)
        {
            reader = new FTRReader(filepath);
            validator = new FTRValidator();
            parser = new FTRParser();
            linker = new FTRLinker();
        }

        public List<Object> Load()
        {
            Dictionary<String, String[]> records = new Dictionary<String, String[]>();
            List<Object> objects = new List<Object>();

            String[]? s;
            while ((s = reader.Read()) != null)
            {
                if (!records.TryAdd(s[1], s))
                    throw new DataIntegrityException("Two objects can't have the same ID.");
            }
            reader.Reset();

            validator.Validate(records);
            try
            {
                parser.Parse(records, objects);
            }
            catch (FormatException e)
            {
                throw new DataIntegrityException("Invalid property format.", e.InnerException);
            }
            linker.Link(records, objects);

            return objects;
        }
    }
}
