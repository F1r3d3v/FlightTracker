namespace ProjOb.Accessors
{
    public class AirportAccessor : ObjectAccessor
    {
        public AirportAccessor(Ref<Airport?> airport) : base(new Ref<Object?>(()=>airport.Value, (x)=>airport.Value = (Airport?)x))
        {
            _getValueTypeMap.Add("Name", () => airport.Value!.Name);
            _setValueMap.Add("Name", (String value) => airport.Value!.Name = value);

            _getValueTypeMap.Add("Code", () => airport.Value!.Code);
            _setValueMap.Add("Code", (String value) => airport.Value!.Code = value);

            _getValueTypeMap.Add("AMSL", () => airport.Value!.AMSL.ToString());
            _setValueMap.Add("AMSL", (String value) => airport.Value!.AMSL = Single.Parse(value));

            _getValueTypeMap.Add("Country", () => airport.Value!.Country);
            _setValueMap.Add("Country", (String value) => airport.Value!.Country = value);

            _accessorMap.Add("WorldPosition", new PositionAccessor(
                new Ref<Single>(() => airport.Value!.Longitude, (Single val) => airport.Value!.Longitude = val),
                new Ref<Single>(() => airport.Value!.Latitude, (Single val) => airport.Value!.Latitude = val)
            ));
        }
    }
}
