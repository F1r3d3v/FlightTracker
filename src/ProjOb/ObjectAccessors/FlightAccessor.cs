namespace ProjOb.Query.Wrappers
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

            _accessorMap.Add("WorldPosition", new WorldPositionAccessor(new WorldPosition(flight.Latitude, flight.Longitude)));
            _accessorMap.Add("Origin", new AirportAccessor(flight.Origin!));
            _accessorMap.Add("Target", new AirportAccessor(flight.Target!));
            _accessorMap.Add("Plane", new PlaneAccessor(flight.Plane!));

            //_getRefTypeMap.Add("WorldPosition", (String? value = null) =>
            //{
            //    var pos = new WorldPosition(flight.Latitude, flight.Longitude);
            //    return new WorldPositionAccessor(pos).GetValue(value ?? "*");
            //});
            //_setValueMap.Add("WorldPosition", (String value, String? path = null) =>
            //{
            //    var pos = new WorldPosition(flight.Latitude, flight.Longitude);
            //    var accessor = new WorldPositionAccessor(pos);
            //    accessor.SetValue(path ?? "*", value);
            //    flight.Longitude = (float)accessor.Position.Longitude;
            //    flight.Latitude = (float)accessor.Position.Latitude;
            //});

            //_getRefTypeMap.Add("Origin", (String? value = null) =>
            //{
            //    return new AirportAccessor(flight.Origin!).GetValue(value ?? "*");
            //});
            //_setValueMap.Add("Origin", (String value, String? path = null) =>
            //{
            //    var accessor = new AirportAccessor(flight.Origin!);
            //    accessor.SetValue(path ?? "*", value);
            //});

            //_getRefTypeMap.Add("Target", (String? value = null) =>
            //{
            //    return new AirportAccessor(flight.Target!).GetValue(value ?? "*");
            //});
            //_setValueMap.Add("Target", (String value, String? path = null) =>
            //{
            //    var accessor = new AirportAccessor(flight.Target!);
            //    accessor.SetValue(path ?? "*", value);
            //});

            //_getRefTypeMap.Add("Plane", (String? value = null) =>
            //{
            //    return new PlaneAccessor(flight.Plane!).GetValue(value ?? "*");
            //});
            //_setValueMap.Add("Plane", (String value, String? path = null) =>
            //{
            //    var accessor = new PlaneAccessor(flight.Plane!);
            //    accessor.SetValue(path ?? "*", value);
            //});
        }
    }
}
