namespace ProjOb.Accessors
{
    public class CargoPlaneAccessor : PlaneAccessor
    {
        public CargoPlaneAccessor(CargoPlane? cargoPlane) : base(cargoPlane)
        {
            if (cargoPlane == null) return;

            _getValueTypeMap.Add("MaxLoad", () => cargoPlane.MaxLoad.ToString());
            _setValueMap.Add("MaxLoad", (String value) => cargoPlane.MaxLoad = Single.Parse(value));
        }
    }
}
