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

        public void Add(dynamic obj)
        {
            var dict = new Dictionary<string, dynamic>()
            {
                { "Crew", Crews },
                { "Passenger", Passengers },
                { "Cargo", Cargos },
                { "CargoPlane", CargoPlanes },
                { "PassengerPlane", PassengerPlanes },
                { "Airport", Airports },
                { "Flight", Flights },
            };

            dict[obj.ToString()].Add(obj);
        }
    }
}
