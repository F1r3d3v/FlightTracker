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
            _validator = new FTRValidator(_reader);
            _parser = new FTRParser();
            _linker = new FTRLinker();
        }

        public Database LoadToDatabase()
        {
            Database database = new Database();
            IComponent dbComp = new DatabaseComponent(database);

            _validator.Validate(out Dictionary<String, String[]> records);

            foreach (var rec in records)
            {
                Object obj = _parser.Parse(rec.Value);
                obj.Apply(dbComp);
            }

            _linker.Link(records, database);

            return database;
        }
    }
}
