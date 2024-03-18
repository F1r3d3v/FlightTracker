using System.Xml.Linq;

namespace ProjOb.IO
{
    internal class FTRFlightFactory : FTRObjectFactory
    {
        protected TimeSpan _takeofftime;
        protected TimeSpan _landingtime;
        protected Single _longitude;
        protected Single _latitude;
        protected Single _amsl;

        public override Flight Create()
        {
            Flight flight = new Flight();
            flight.ID = _id;
            flight.Origin = null;
            flight.Target = null;
            flight.TakeoffTime = _takeofftime;
            flight.LandingTime = _landingtime;
            flight.Longitude = _longitude;
            flight.Latitude = _latitude;
            flight.AMSL = _amsl;
            flight.Plane = null;
            return flight;
        }

        public override void Populate(String[] props)
        {
            try
            {
                base.Populate(props);
                _takeofftime = TimeSpan.Parse(props[3]);
                _landingtime = TimeSpan.Parse(props[4]);
                _longitude = Single.Parse(props[5]);
                _latitude = Single.Parse(props[6]);
                _amsl = Single.Parse(props[7]);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the flight object: {e.Message}", e);
            }
        }
    }
}
