namespace ProjOb.Components
{
    public class DatabaseComponent : IComponent
    {
        private Database _database;
        public DatabaseComponent(Database database)
        {
            _database = database;
        }

        public void Process(Crew crew)
        {
            _database.Crews.Add(crew);
        }

        public void Process(Passenger passengger)
        {
            _database.Passengers.Add(passengger);
        }

        public void Process(Cargo cargo)
        {
            _database.Cargos.Add(cargo);
        }

        public void Process(CargoPlane cargoPlane)
        {
            _database.CargoPlanes.Add(cargoPlane);
        }

        public void Process(PassengerPlane passengerPlane)
        {
            _database.PassengerPlanes.Add(passengerPlane);
        }

        public void Process(Airport airport)
        {
            _database.Airports.Add(airport);
        }

        public void Process(Flight flight)
        {
            _database.Flights.Add(flight);
        }
    }
}
