namespace ProjOb.Query.Wrappers
{
    public class AirportAccessor : ObjectAccessor
    {
        public AirportAccessor(Airport airport) : base(airport)
        {
            _getValueTypeMap.Add("Name", () => airport.Name);
            _setValueMap.Add("Name", (String value) => airport.Name = value);

            _getValueTypeMap.Add("Code", () => airport.Code);
            _setValueMap.Add("Code", (String value) => airport.Code = value);

            _getValueTypeMap.Add("AMSL", () => airport.AMSL.ToString());
            _setValueMap.Add("AMSL", (String value) => airport.AMSL = Single.Parse(value));

            _getValueTypeMap.Add("Country", () => airport.Country);
            _setValueMap.Add("Country", (String value) => airport.Country = value);

            _accessorMap.Add("WorldPosition", new WorldPositionAccessor(new WorldPosition(airport.Latitude, airport.Longitude)));
        }
    }
}
