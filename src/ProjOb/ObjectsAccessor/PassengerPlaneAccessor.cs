namespace ProjOb.Accessors
{
    public class PassengerPlaneAccessor : PlaneAccessor
    {
        public PassengerPlaneAccessor(Ref<PassengerPlane?> passengerPlane) : base(new Ref<Plane?>(() => passengerPlane.Value, (x) => passengerPlane.Value = (PassengerPlane?)x))
        {
            _getValueTypeMap.Add("FirstClassSize", () => passengerPlane.Value!.FirstClassSize.ToString());
            _setValueMap.Add("FirstClassSize", (String value) => passengerPlane.Value!.FirstClassSize = UInt16.Parse(value));

            _getValueTypeMap.Add("BusinessClassSize", () => passengerPlane.Value!.BusinessClassSize.ToString());
            _setValueMap.Add("BusinessClassSize", (String value) => passengerPlane.Value!.BusinessClassSize = UInt16.Parse(value));

            _getValueTypeMap.Add("EconomyClassSize", () => passengerPlane.Value!.EconomyClassSize.ToString());
            _setValueMap.Add("EconomyClassSize", (String value) => passengerPlane.Value!.EconomyClassSize = UInt16.Parse(value));
        }
    }
}
