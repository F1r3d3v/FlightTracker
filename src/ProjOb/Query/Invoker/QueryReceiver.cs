using ProjOb.Objects;
using ProjOb.Query.Wrappers;
using System.Collections.Generic;

namespace ProjOb.Query.Invoker
{
    public class QueryReceiver
    {
        private readonly Database _db;
        private readonly Dictionary<ObjectClassEnum, ICollection<Object>> _objMap;

        public QueryReceiver(Database db)
        {
            _db = db;
            _objMap = new()
            {
                [ObjectClassEnum.Crews] = _db.Crews.Values.ToArray(),
                [ObjectClassEnum.Passengers] = _db.Passengers.Values.ToArray(),
                [ObjectClassEnum.Cargos] = _db.Cargos.Values.ToArray(),
                [ObjectClassEnum.CargoPlanes] = _db.CargoPlanes.Values.ToArray(),
                [ObjectClassEnum.PassengerPlanes] = _db.PassengerPlanes.Values.ToArray(),
                [ObjectClassEnum.Airports] = _db.Airports.Values.ToArray(),
                [ObjectClassEnum.Flights] = _db.Flights.Values.ToArray(),
            };
        }

        public QueryResult DisplayAction(ObjectClassEnum objectClass, String[]? varlist, Func<Object, bool> predicate)
        {
            List<Dictionary<String, String>> records = [];
            foreach (Object obj in _objMap[objectClass])
            {
                if (!predicate(obj)) continue;

                IQueryAccessor accessor = obj.Apply(new AccessorVisitor());
                Dictionary<String, String> varlistPair = [];

                foreach (String var in accessor.GetFields())
                {
                    String? value = accessor.GetValue(var);
                    if (value != null && (varlist == null || varlist.Contains(var, StringComparer.OrdinalIgnoreCase)))
                        varlistPair.Add(var, value);
                }

                records.Add(varlistPair);
            }

            return new QueryResult(records);
        }

        public void UpdateAction(ObjectClassEnum objectClass, KeyValuePair<String, String> varlist, Func<Object, bool> predicate)
        {
        }

        public void RemoveAction(ObjectClassEnum objectClass, Func<Object, bool>? predicate)
        {
        }

        public void AddAction(ObjectClassEnum objectClass, KeyValuePair<String, String> varlist)
        {
        }
    }
}
