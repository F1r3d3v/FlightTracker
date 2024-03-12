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
            _database.Crews.Add(crew.ID, crew);
        }

        public void Process(Passenger passengger)
        {
            _database.Passengers.Add(passengger.ID, passengger);
        }

        public void Process(Cargo cargo)
        {
            _database.Cargos.Add(cargo.ID, cargo);
        }

        public void Process(CargoPlane cargoPlane)
        {
            _database.CargoPlanes.Add(cargoPlane.ID, cargoPlane);
        }

        public void Process(PassengerPlane passengerPlane)
        {
            _database.PassengerPlanes.Add(passengerPlane.ID, passengerPlane);
        }

        public void Process(Airport airport)
        {
            _database.Airports.Add(airport.ID, airport);
        }

        public void Process(Flight flight)
        {
            _database.Flights.Add(flight.ID, flight);
        }
    }
}
