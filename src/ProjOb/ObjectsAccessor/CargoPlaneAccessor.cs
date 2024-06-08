namespace ProjOb.Accessors
{
    public class CargoPlaneAccessor : PlaneAccessor
    {
        public CargoPlaneAccessor(Ref<CargoPlane?> cargoPlane) : base(new Ref<Plane?>(() => cargoPlane.Value, (x) => cargoPlane.Value = (CargoPlane?)x))
        {
            _getValueTypeMap.Add("MaxLoad", () => cargoPlane.Value!.MaxLoad.ToString());
            _setValueMap.Add("MaxLoad", (String value) => cargoPlane.Value!.MaxLoad = Single.Parse(value));
        }
    }
}
