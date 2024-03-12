using NetworkSourceSimulator;
using System.Xml.Linq;

namespace ProjOb.IO
{
    internal class NSSFlightFactory : NSSObjectFactory
    {
        protected String? _takeofftime;
        protected String? _landingtime;

        public override Flight Create()
        {
            Flight flight = new Flight();
            flight.ID = _id;
            flight.Origin = null;
            flight.Target = null;
            flight.TakeoffTime = _takeofftime;
            flight.LandingTime = _landingtime;
            flight.Longitude = 0;
            flight.Latitude = 0;
            flight.AMSL = 0;
            flight.Plane = null;
            return flight;
        }

        public override void Populate(Byte[] msg)
        {
            try
            {
                base.Populate(msg);

                Int64 takeofftime = BitConverter.ToInt64(msg, 31);
                DateTime takeoffDate = DateTimeOffset.FromUnixTimeMilliseconds(takeofftime).DateTime;
                _takeofftime = takeoffDate.ToString("HH:mm");

                Int64 landingtime = BitConverter.ToInt64(msg, 39);
                DateTime landingDate = DateTimeOffset.FromUnixTimeMilliseconds(landingtime).DateTime;
                _landingtime = landingDate.ToString("HH:mm");
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the flight object: {e.Message}", e);
            }
        }
    }
}
