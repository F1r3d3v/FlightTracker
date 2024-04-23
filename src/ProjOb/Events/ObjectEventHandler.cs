using ProjOb.IO;
using NetworkSourceSimulator;
using ProjOb.Components;


namespace ProjOb.Events
{
    internal class ObjectEventHandler : IObjectObserver
    {
        private Database _db;

        public ObjectEventHandler(Database db)
        {
            _db = db;
        }

        public void OnIDChanged(object sender, IDUpdateArgs args)
        {
            if (args.ObjectID == args.NewObjectID) return;

            Object? obj = null;
            foreach (Object val in _db.GetObjects())
            {
                if (val.ID == args.ObjectID)
                    obj = val;

                if (val.ID == args.NewObjectID)
                {
                    Logger.ErrorAsync("Cannot change object ID: IDs conflict");
                    return;
                }
            }

            if (obj == null)
            {
                Logger.ErrorAsync("Cannot change object ID: No object entry in database");
                return;
            }

            obj.Apply(new RemoveFromDatabaseComponent(_db));
            lock (obj)
            {
                obj.OnIDChanged(sender, args);
            }
            obj.Apply(new AddToDatabaseComponent(_db));
        }

        public void OnPositionChanged(object sender, PositionUpdateArgs args)
        {
            Object? obj = _db.GetObject(args.ObjectID);

            if (obj == null)
            {
                Logger.ErrorAsync("Cannot change object Position: No object entry in database");
                return;
            }

            lock (obj)
            {
                obj.OnPositionChanged(sender, args);
            }
        }

        public void OnContactInfoChanged(object sender, ContactInfoUpdateArgs args)
        {
            Object? obj = _db.GetObject(args.ObjectID);

            if (obj == null)
            {
                Logger.ErrorAsync("Cannot change object Contact Info: No object entry in database");
                return;
            }

            lock (obj)
            {
                obj.OnContactInfoChanged(sender, args);
            }
        }
    }
}
