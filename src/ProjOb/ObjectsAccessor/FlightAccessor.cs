namespace ProjOb.Accessors
{
    public class FlightAccessor : ObjectAccessor
    {
        public FlightAccessor(Flight flight) : base(flight)
        {
            _getValueTypeMap.Add("TakeoffTime", () => flight.TakeoffTime.ToString());
            _setValueMap.Add("TakeoffTime", (String value) => flight.TakeoffTime = TimeSpan.Parse(value));

            _getValueTypeMap.Add("LandingTime", () => flight.LandingTime.ToString());
            _setValueMap.Add("LandingTime", (String value) => flight.LandingTime = TimeSpan.Parse(value));

            _getValueTypeMap.Add("AMSL", () => flight.AMSL.ToString());
            _setValueMap.Add("AMSL", (String value) => flight.AMSL = Single.Parse(value));

            _accessorMap.Add("WorldPosition", new PositionAccessor(
                new Ref<Single>(() => flight.Longitude, (Single val) => flight.Longitude = val),
                new Ref<Single>(() => flight.Latitude, (Single val) => flight.Latitude = val)
            ));

            _accessorMap.Add("Origin", new AirportAccessor(flight.Origin!));
            _accessorMap.Add("Target", new AirportAccessor(flight.Target!));
            _accessorMap.Add("Plane", new PlaneAccessor(flight.Plane!));
        }
    }
}
