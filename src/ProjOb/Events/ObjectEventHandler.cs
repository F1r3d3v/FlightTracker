using ProjOb.IO;
using NetworkSourceSimulator;


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
            Object? obj = null;

            foreach (Object val in _db)
            {
                if (val.ID == args.ObjectID)
                    obj = val;

                if (val.ID == args.NewObjectID)
                {
                    Logger.Error("Cannot change object ID: IDs conflict");
                    return;
                }

            }

            obj?.OnIDChanged(sender, args);
        }

        public void OnPositionChanged(object sender, PositionUpdateArgs args)
        {
            Object? obj = _db.GetObject(args.ObjectID);
            obj?.OnPositionChanged(sender, args);
        }

        public void OnContactInfoChanged(object sender, ContactInfoUpdateArgs args)
        {
            Object? obj = _db.GetObject(args.ObjectID);
            obj?.OnContactInfoChanged(sender, args);
        }
    }
}
