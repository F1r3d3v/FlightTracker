namespace ProjOb.Accessors
{
    public class FlightAccessor : ObjectAccessor
    {
        public FlightAccessor(Ref<Flight?> flight) : base(new Ref<Object?>(() => flight.Value, (x) => flight.Value = (Flight?)x))
        {
            _getValueTypeMap.Add("TakeoffTime", () => flight.Value!.TakeoffTime.ToString());
            _setValueMap.Add("TakeoffTime", (String value) => flight.Value!.TakeoffTime = TimeSpan.Parse(value));

            _getValueTypeMap.Add("LandingTime", () => flight.Value!.LandingTime.ToString());
            _setValueMap.Add("LandingTime", (String value) => flight.Value!.LandingTime = TimeSpan.Parse(value));

            _getValueTypeMap.Add("AMSL", () => flight.Value!.AMSL.ToString());
            _setValueMap.Add("AMSL", (String value) => flight.Value!.AMSL = Single.Parse(value));

            _accessorMap.Add("WorldPosition", new PositionAccessor(
                new Ref<Single>(() => flight.Value!.Longitude, (Single val) => flight.Value!.Longitude = val),
                new Ref<Single>(() => flight.Value!.Latitude, (Single val) => flight.Value!.Latitude = val)
            ));

            _accessorMap.Add("Plane", new PlaneAccessor(new Ref<Plane?>(() => flight.Value!.Plane, (x) => flight.Value!.Plane = x)));
            _accessorMap.Add("Origin", new AirportAccessor(new Ref<Airport?>(() => flight.Value!.Origin, (x) => flight.Value!.Origin = x)));
            _accessorMap.Add("Target", new AirportAccessor(new Ref<Airport?>(() => flight.Value!.Target, (x) => flight.Value!.Target = x)));
        }
    }
}
