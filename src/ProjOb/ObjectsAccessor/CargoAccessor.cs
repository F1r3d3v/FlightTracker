namespace ProjOb.Accessors
{
    public class CargoAccessor : ObjectAccessor
    {
        public CargoAccessor(Ref<Cargo?> cargo) : base(new Ref<Object?>(() => cargo.Value, (x) => cargo.Value = (Cargo?)x))
        {
            _getValueTypeMap.Add("Weight", () => cargo.Value!.Weight.ToString());
            _setValueMap.Add("Weight", (String value) => cargo.Value!.Weight = Single.Parse(value));

            _getValueTypeMap.Add("Code", () => cargo.Value!.Code);
            _setValueMap.Add("Code", (String value) => cargo.Value!.Code = value);

            _getValueTypeMap.Add("Description", () => cargo.Value!.Description);
            _setValueMap.Add("Description", (String value) => cargo.Value!.Description = value);
        }
    }
}
