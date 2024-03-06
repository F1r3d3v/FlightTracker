using ProjOb.Exceptions;
using ProjOb.Components;

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

        public Database LoadToDatabase()
        {
            Database database = new Database();
            IComponent dbComp = new DatabaseComponent(database);

            Dictionary<String, String[]> records = [];
            List<Object> objects = new List<Object>();

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
                objects = _parser!.Parse(records);
            }
            catch (FormatException e)
            {
                throw new DataIntegrityException("Invalid property format.", e.InnerException);
            }

            objects.ForEach(x => x.Apply(dbComp));

            _linker.Link(records, database);

            return database;
        }
    }
}
