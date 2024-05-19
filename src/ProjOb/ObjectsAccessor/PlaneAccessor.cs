namespace ProjOb.Accessors
{
    public class PlaneAccessor : ObjectAccessor
    {
        public PlaneAccessor(Plane plane) : base(plane)
        {
            _getValueTypeMap.Add("Serial", () => plane.Serial);
            _setValueMap.Add("Serial", (String value) => plane.Serial = value);

            _getValueTypeMap.Add("Country", () => plane.Country);
            _setValueMap.Add("Country", (String value) => plane.Country = value);

            _getValueTypeMap.Add("Model", () => plane.Model);
            _setValueMap.Add("Model", (String value) => plane.Model = value);
        }
    }
}
