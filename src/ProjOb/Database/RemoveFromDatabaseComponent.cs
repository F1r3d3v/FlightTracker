namespace ProjOb.Components
{
    public class RemoveFromDatabaseComponent : IComponent
    {
        private Database _database;

        public RemoveFromDatabaseComponent(Database database)
        {
            _database = database;
        }

        public void Process(Crew crew)
        {
            _database.Crews.Remove(crew.ID);
        }

        public void Process(Passenger passengger)
        {
            _database.Passengers.Remove(passengger.ID);
        }

        public void Process(Cargo cargo)
        {
            _database.Cargos.Remove(cargo.ID);
        }

        public void Process(CargoPlane cargoPlane)
        {
            _database.CargoPlanes.Remove(cargoPlane.ID);
        }

        public void Process(PassengerPlane passengerPlane)
        {
            _database.PassengerPlanes.Remove(passengerPlane.ID);
        }

        public void Process(Airport airport)
        {
            _database.Airports.Remove(airport.ID);
        }

        public void Process(Flight flight)
        {
            _database.Flights.Remove(flight.ID);
        }
    }
}
