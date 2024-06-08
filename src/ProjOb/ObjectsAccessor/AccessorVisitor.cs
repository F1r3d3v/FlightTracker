namespace ProjOb.Accessors
{
    internal class AccessorVisitor : IComponent<IQueryAccessor>
    {
        public IQueryAccessor Process(Crew? crew) => new CrewAccessor(new Ref<Crew?>(() => crew, (x) => crew = x));
        public IQueryAccessor Process(Passenger? passengger) => new PassengerAccessor(new Ref<Passenger?>(() => passengger, (x) => passengger = x));
        public IQueryAccessor Process(Cargo? cargo) => new CargoAccessor(new Ref<Cargo?>(() => cargo, (x) => cargo = x));
        public IQueryAccessor Process(CargoPlane? cargoPlane) => new CargoPlaneAccessor(new Ref<CargoPlane?>(() => cargoPlane, (x) => cargoPlane = x));
        public IQueryAccessor Process(PassengerPlane? passengerPlane) => new PassengerPlaneAccessor(new Ref<PassengerPlane?>(() => passengerPlane, (x) => passengerPlane = x));
        public IQueryAccessor Process(Airport? airport) => new AirportAccessor(new Ref<Airport?>(() => airport, (x) => airport = x));
        public IQueryAccessor Process(Flight? flight) => new FlightAccessor(new Ref<Flight?>(() => flight, (x) => flight = x));
    }
}
