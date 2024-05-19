namespace ProjOb.Accessors
{
    internal class AccessorVisitor : IComponent<IQueryAccessor>
    {
        public IQueryAccessor Process(Crew crew) => new CrewAccessor(crew);
        public IQueryAccessor Process(Passenger passengger) => new PassengerAccessor(passengger);
        public IQueryAccessor Process(Cargo cargo) => new CargoAccessor(cargo);
        public IQueryAccessor Process(CargoPlane cargoPlane) => new CargoPlaneAccessor(cargoPlane);
        public IQueryAccessor Process(PassengerPlane passengerPlane) => new PassengerPlaneAccessor(passengerPlane);
        public IQueryAccessor Process(Airport airport) => new AirportAccessor(airport);
        public IQueryAccessor Process(Flight flight) => new FlightAccessor(flight);
    }
}
