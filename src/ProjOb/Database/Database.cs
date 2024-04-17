using ProjOb.IO;
using System.Collections;

namespace ProjOb
{
    public class Database : IEnumerable<Object>
    {
        [JsonOnlyDictVal] public Dictionary<UInt64, Crew> Crews { get; private set; } = [];
        [JsonOnlyDictVal] public Dictionary<UInt64, Passenger> Passengers { get; private set; } = [];
        [JsonOnlyDictVal] public Dictionary<UInt64, Cargo> Cargos { get; private set; } = [];
        [JsonOnlyDictVal] public Dictionary<UInt64, CargoPlane> CargoPlanes { get; private set; } = [];
        [JsonOnlyDictVal] public Dictionary<UInt64, PassengerPlane> PassengerPlanes { get; private set; } = [];
        [JsonOnlyDictVal] public Dictionary<UInt64, Airport> Airports { get; private set; } = [];
        [JsonOnlyDictVal] public Dictionary<UInt64, Flight> Flights { get; private set; } = [];

        public void Serialize(String filepath)
        {
            IWriter wr = WriterFactory.Create(filepath) ?? throw new Exception("Can't create a file writer.");

            wr.Write(this);
            wr.Close();
        }

        public static Database Deserialize(String filepath)
        {
            ILoader loader = LoaderFactory.CreateLoader(filepath);
            Database db = new Database();
            loader.LoadToDatabase(db);
            return db;
        }

        public Object? GetObject(UInt64 id)
        {
           Dictionary<UInt64, Object>[] dicts = [
                Crews.ToDictionary(x => x.Key, x => (Object)x.Value),
                Passengers.ToDictionary(x => x.Key, x => (Object)x.Value),
                Cargos.ToDictionary(x => x.Key, x => (Object)x.Value),
                CargoPlanes.ToDictionary(x => x.Key, x => (Object)x.Value),
                PassengerPlanes.ToDictionary(x => x.Key, x => (Object)x.Value),
                Airports.ToDictionary(x => x.Key, x => (Object)x.Value),
                Flights.ToDictionary(x => x.Key, x => (Object)x.Value)
            ];

            foreach (var dict in dicts)
            {
                if (dict.TryGetValue(id, out Object? obj))
                {
                    return obj;
                }
            }

            return null;
        }

        public IEnumerator<Object> GetEnumerator()
        {
            Object[] objects = [
                .. Crews.Values,
                .. Passengers.Values,
                .. Cargos.Values,
                .. CargoPlanes.Values,
                .. PassengerPlanes.Values,
                .. Airports.Values,
                .. Flights.Values
            ];

            foreach (Object obj in objects)
            {
                yield return obj;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
