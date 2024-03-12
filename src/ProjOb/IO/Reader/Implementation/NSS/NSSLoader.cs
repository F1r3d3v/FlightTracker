using ProjOb.Components;
using NetworkSourceSimulator;

namespace ProjOb.IO
{
    internal class NSSLoader : ILoader
    {
        private readonly NSSParser _parser;
        private readonly NSSLinker _linker;
        private NetworkSourceSimulator.NetworkSourceSimulator _nss;
        private String _filepath;
        private Database? _db;

        internal NSSLoader(String filepath, int minDelay = 100, int maxDelay = 250)
        {
            _nss = new(filepath, minDelay, maxDelay);
            _nss.OnNewDataReady += AddToDatabase;
            _parser = new NSSParser();
            _linker = new NSSLinker();
            _filepath = filepath;
        }

        public void LoadToDatabase(Database db)
        {
            _db = db;
            _nss.Run();
        }

        private void AddToDatabase(object sender, NewDataReadyArgs args)
        {
            Message msg = _nss.GetMessageAt(args.MessageIndex);
            Object obj = _parser.Parse(msg.MessageBytes);

            IComponent dbComp = new DatabaseComponent(_db!);
            lock(_db!)
            {
                obj.Apply(dbComp);
                _linker.Link(msg.MessageBytes, _db);
            }
        }
    }
}
