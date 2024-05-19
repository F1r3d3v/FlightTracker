namespace ProjOb.Accessors
{
    public class CargoAccessor : ObjectAccessor
    {
        public CargoAccessor(Cargo cargo) : base(cargo)
        {
            _getValueTypeMap.Add("Weight", () => cargo.Weight.ToString());
            _setValueMap.Add("Weight", (String value) => cargo.Weight = Single.Parse(value));

            _getValueTypeMap.Add("Code", () => cargo.Code);
            _setValueMap.Add("Code", (String value) => cargo.Code = value);

            _getValueTypeMap.Add("Description", () => cargo.Description);
            _setValueMap.Add("Description", (String value) => cargo.Description = value);
        }
    }
}
