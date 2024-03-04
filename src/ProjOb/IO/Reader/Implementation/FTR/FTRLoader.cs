using ProjOb.Exceptions;

namespace ProjOb.IO
{
    internal class FTRLoader : ILoader
    {
        private readonly FTRReader _reader;
        private readonly FTRValidator _validator;
        private readonly FTRParser _parser;
        private readonly FTRLinker _linker;

        internal FTRLoader(String filepath)
        {
            _reader = new FTRReader(filepath);
            _validator = new FTRValidator();
            _parser = new FTRParser();
            _linker = new FTRLinker();
        }

        public void LoadToDatabase(out Database database)
        {
            database = new Database();
            Dictionary<String, String[]> records = [];

            String[]? s;
            while ((s = _reader!.Read()) != null)
            {
                if (!records.TryAdd(s[1], s))
                    throw new DataIntegrityException("Two objects can't have the same ID.");
            }
            _reader.Reset();

            _validator!.Validate(records);
            try
            {
                _parser!.Parse(records, database);
            }
            catch (FormatException e)
            {
                throw new DataIntegrityException("Invalid property format.", e.InnerException);
            }
            _linker.Link(records, database);
        }
    }
}
