using ProjOb.Exceptions;

namespace ProjOb.Components
{
    public class AddToDatabaseComponent : IComponent
    {
        private Database _database;

        public AddToDatabaseComponent(Database database)
        {
            _database = database;
        }

        private void CheckIntegrity(Object obj)
        {
            foreach (Object val in _database.GetObjects())
            {
                if (val.ID == obj.ID)
                    throw new DataIntegrityException($"Object with ID: {val.ID} is currently in the database");
            }
        }

        public void Process(Crew crew)
        {
            CheckIntegrity(crew);
            _database.Crews.Add(crew.ID, crew);
        }

        public void Process(Passenger passengger)
        {
            CheckIntegrity(passengger);
            _database.Passengers.Add(passengger.ID, passengger);
        }

        public void Process(Cargo cargo)
        {
            CheckIntegrity(cargo);
            _database.Cargos.Add(cargo.ID, cargo);
        }

        public void Process(CargoPlane cargoPlane)
        {
            CheckIntegrity(cargoPlane);
            _database.CargoPlanes.Add(cargoPlane.ID, cargoPlane);
        }

        public void Process(PassengerPlane passengerPlane)
        {
            CheckIntegrity(passengerPlane);
            _database.PassengerPlanes.Add(passengerPlane.ID, passengerPlane);
        }

        public void Process(Airport airport)
        {
            CheckIntegrity(airport);
            _database.Airports.Add(airport.ID, airport);
        }

        public void Process(Flight flight)
        {
            CheckIntegrity(flight);
            _database.Flights.Add(flight.ID, flight);
        }
    }
}
