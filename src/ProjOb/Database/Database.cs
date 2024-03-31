using ProjOb.IO;

namespace ProjOb
{
    public class Database
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
    }
}
