namespace ProjOb.IO
{
    internal class FTRPassengerPlaneFactory : FTRPlaneFactory
    {
        protected UInt16 _firstclasssize;
        protected UInt16 _businessclasssize;
        protected UInt16 _economyclasssize;

        public override PassengerPlane Create()
        {
            PassengerPlane passengerplane = new PassengerPlane();
            passengerplane.ID = _id;
            passengerplane.Serial = _serial;
            passengerplane.Country = _country;
            passengerplane.Model = _model;
            passengerplane.FirstClassSize = _firstclasssize;
            passengerplane.BusinessClassSize = _businessclasssize;
            passengerplane.EconomyClassSize = _economyclasssize;
            return passengerplane;
        }

        public override void Populate(String[] props)
        {
            try
            {
                base.Populate(props);
                _firstclasssize = UInt16.Parse(props[4]);
                _businessclasssize = UInt16.Parse(props[5]);
                _economyclasssize = UInt16.Parse(props[6]);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the passenger plane object: {e.Message}", e);
            }
        }
    }
}
