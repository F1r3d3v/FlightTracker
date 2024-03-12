namespace ProjOb.IO
{
    internal class FTRAirportFactory : FTRObjectFactory
    {
        protected String? _name;
        protected String? _code;
        protected Single _longitude;
        protected Single _latitude;
        protected Single _amsl;
        protected String? _country;

        public override Airport Create()
        {
            Airport airport = new Airport();
            airport.ID = _id;
            airport.Name = _name;
            airport.Code = _code;
            airport.Longitude = _longitude;
            airport.Latitude = _latitude;
            airport.AMSL = _amsl;
            airport.Country = _country;
            return airport;
        }

        public override void Populate(String[] props)
        {
            try
            {
                base.Populate(props);
                _name = props[1];
                _code = props[2];
                _longitude = Single.Parse(props[3]);
                _latitude = Single.Parse(props[4]);
                _amsl = Single.Parse(props[5]);
                _country = props[6];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the airport object: {e.Message}", e);
            }
        }
    }
}
