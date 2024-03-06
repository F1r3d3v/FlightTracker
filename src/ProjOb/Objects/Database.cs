using ProjOb.IO;

namespace ProjOb
{
    public class Database
    {
        public List<Crew> Crews { get; private set; } = [];
        public List<Passenger> Passengers { get; private set; } = [];
        public List<Cargo> Cargos { get; private set; } = [];
        public List<CargoPlane> CargoPlanes { get; private set; } = [];
        public List<PassengerPlane> PassengerPlanes { get; private set; } = [];
        public List<Airport> Airports { get; private set; } = [];
        public List<Flight> Flights { get; private set; } = [];

        public void Serialize(String filepath)
        {
            IWriter wr = WriterFactory.Create(filepath) ?? throw new Exception("Can't create a file writer.");

            wr.Write(this);
            wr.Close();
        }

        public static Database Deserialize(String filepath)
        {
            ILoader loader = LoaderFactory.CreateLoader(filepath);
            return loader.LoadToDatabase();
        }
    }
}
