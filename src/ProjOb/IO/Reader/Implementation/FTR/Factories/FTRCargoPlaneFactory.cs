namespace ProjOb.IO
{
    internal class FTRCargoPlaneFactory : FTRPlaneFactory
    {
        protected Single _maxload;

        public override CargoPlane Create()
        {
            CargoPlane cargoplane = new CargoPlane();
            cargoplane.ID = _id;
            cargoplane.Serial = _serial;
            cargoplane.Country = _country;
            cargoplane.Model = _model;
            cargoplane.MaxLoad = _maxload;
            return cargoplane;
        }

        public override void Populate(String[] data)
        {
            try
            {
                base.Populate(data);
                _maxload = Single.Parse(data[4]);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the cargo plane object: {e.Message}", e);
            }
        }
    }
}
