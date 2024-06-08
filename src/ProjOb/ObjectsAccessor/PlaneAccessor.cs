namespace ProjOb.Accessors
{
    public class PlaneAccessor : ObjectAccessor
    {
        public PlaneAccessor(Ref<Plane?> plane) : base(new Ref<Object?>(() => plane.Value, (x) => plane.Value = (Plane?)x))
        {
            _getValueTypeMap.Add("Serial", () => plane.Value!.Serial);
            _setValueMap.Add("Serial", (String value) => plane.Value!.Serial = value);

            _getValueTypeMap.Add("Country", () => plane.Value!.Country);
            _setValueMap.Add("Country", (String value) => plane.Value!.Country = value);

            _getValueTypeMap.Add("Model", () => plane.Value!.Model);
            _setValueMap.Add("Model", (String value) => plane.Value!.Model = value);
        }
    }
}
