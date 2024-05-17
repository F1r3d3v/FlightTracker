using NetworkSourceSimulator;
using ProjOb.Components;
using ProjOb.Events;

namespace ProjOb.IO
{
    internal class NSSLoader : ILoader
    {
        private readonly NSSParser _parser;
        private readonly NSSLinker _linker;
        private NetworkSourceSimulator.NetworkSourceSimulator _nss;
        private String _filepath;
        private Database? _db;

        internal NSSLoader(String filepath, int minDelay, int maxDelay)
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
            ObjectEventHandler eventHandler = new ObjectEventHandler(db);
            _nss.OnIDUpdate += eventHandler.OnIDChanged;
            _nss.OnPositionUpdate += eventHandler.OnPositionChanged;
            _nss.OnContactInfoUpdate += eventHandler.OnContactInfoChanged;
            _nss.Run();
        }

        private void AddToDatabase(object sender, NewDataReadyArgs args)
        {
            if (_db == null) return;

            Message msg = _nss.GetMessageAt(args.MessageIndex);
            Object obj = _parser.Parse(msg.MessageBytes);

            IComponent dbComp = new AddToDatabaseComponent(_db);
            lock (_db)
            {
                obj.Apply(dbComp);
                _linker.Link(msg.MessageBytes, _db);
            }
        }
    }
}
