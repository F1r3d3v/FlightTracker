namespace ProjOb.Accessors
{
    public class PassengerPlaneAccessor : PlaneAccessor
    {
        public PassengerPlaneAccessor(PassengerPlane? passengerPlane) : base(passengerPlane)
        {
            if (passengerPlane == null) return;

            _getValueTypeMap.Add("FirstClassSize", () => passengerPlane.FirstClassSize.ToString());
            _setValueMap.Add("FirstClassSize", (String value) => passengerPlane.FirstClassSize = UInt16.Parse(value));

            _getValueTypeMap.Add("BusinessClassSize", () => passengerPlane.BusinessClassSize.ToString());
            _setValueMap.Add("BusinessClassSize", (String value) => passengerPlane.BusinessClassSize = UInt16.Parse(value));

            _getValueTypeMap.Add("EconomyClassSize", () => passengerPlane.EconomyClassSize.ToString());
            _setValueMap.Add("EconomyClassSize", (String value) => passengerPlane.EconomyClassSize = UInt16.Parse(value));
        }
    }
}
