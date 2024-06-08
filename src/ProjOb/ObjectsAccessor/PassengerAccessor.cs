namespace ProjOb.Accessors
{
    public class PassengerAccessor : PersonAccessor
    {
        public PassengerAccessor(Ref<Passenger?> passenger) : base(new Ref<Person?>(() => passenger.Value, (x) => passenger.Value = (Passenger?)x))
        {
            _getValueTypeMap.Add("Class", () => passenger.Value!.Class);
            _setValueMap.Add("Class", (String value) => passenger.Value!.Class = value);

            _getValueTypeMap.Add("Miles", () => passenger.Value!.Miles.ToString());
            _setValueMap.Add("Miles", (String value) => passenger.Value!.Miles = UInt64.Parse(value));
        }
    }
}
