using ProjOb.Components;
using ProjOb.Query.Wrappers;

namespace ProjOb.Query.Invoker
{
    public class QueryReceiver
    {
        private readonly Database _db;
        private readonly Dictionary<ObjectClassEnum, ICollection<Object>> _objMap;
        private readonly Dictionary<ObjectClassEnum, Func<Object>> _objCreationMap;

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
            _objCreationMap = new()
            {
                [ObjectClassEnum.Crews] = () => new Crew(),
                [ObjectClassEnum.Passengers] = () => new Passenger(),
                [ObjectClassEnum.Cargos] = () => new Cargo(),
                [ObjectClassEnum.CargoPlanes] = () => new CargoPlane(),
                [ObjectClassEnum.PassengerPlanes] = () => new PassengerPlane(),
                [ObjectClassEnum.Airports] = () => new Airport(),
                [ObjectClassEnum.Flights] = () => new Flight(),
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

                if (varlist != null)
                {
                    foreach (String var in varlist)
                    {
                        String? value = accessor.GetValue(var);

                        // Keep case-sensitive label from case-insensitive string
                        String[] split = var.Split('.');
                        String[] fieldsNames = new String[split.Length];

                        IEnumerable<String> fields = accessor.GetFields();
                        var equal = (String x) => StringComparer.OrdinalIgnoreCase.Compare(x, split[0]) == 0;
                        fieldsNames[0] = fields.Where(equal).First();

                        for (int i = 1; (i < split.Length) && split.Length > 1; ++i)
                        {
                            fields = accessor.GetFields(split[i - 1]);

                            equal = (String x) => StringComparer.OrdinalIgnoreCase.Compare(x, split[i]) == 0;
                            fieldsNames[i] = fields.Where(equal).First();
                        }

                        varlistPair.Add(String.Join('.', fieldsNames), value ?? "Null");
                    }
                }
                else
                {
                    foreach (String var in accessor.GetFields())
                    {
                        String? value = accessor.GetValue(var);
                        varlistPair.Add(var, value ?? "Null");
                    }
                }

                records.Add(varlistPair);
            }

            return new QueryResult(records);
        }

        public void UpdateAction(ObjectClassEnum objectClass, List<KeyValuePair<String, String>> setlist, Func<Object, bool> predicate)
        {
            foreach (Object obj in _objMap[objectClass])
            {
                if (!predicate(obj)) continue;

                IQueryAccessor accessor = obj.Apply(new AccessorVisitor());
                foreach (var pair in setlist)
                {
                    accessor.SetValue(pair.Key, pair.Value);
                }
            }
        }

        public void DeleteAction(ObjectClassEnum objectClass, Func<Object, bool> predicate)
        {
            foreach (Object obj in _objMap[objectClass])
            {
                if (!predicate(obj)) continue;
                obj.Apply(new RemoveFromDatabaseComponent(_db));
            }
        }

        public void AddAction(ObjectClassEnum objectClass, List<KeyValuePair<String, String>> setlist)
        {
            Object obj = _objCreationMap[objectClass]();
            IQueryAccessor accessor = obj.Apply(new AccessorVisitor());
            foreach (var pair in setlist)
            {
                accessor.SetValue(pair.Key, pair.Value);
            }
            obj.Apply(new AddToDatabaseComponent(_db));
        }
    }
}
