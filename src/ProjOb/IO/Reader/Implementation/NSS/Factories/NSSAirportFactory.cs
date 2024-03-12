using System.Text;

namespace ProjOb.IO
{
    internal class NSSAirportFactory : NSSObjectFactory
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

        public override void Populate(Byte[] msg)
        {
            try
            {
                base.Populate(msg);
                UInt16 nameLength = BitConverter.ToUInt16(msg, 15);
                _name = Encoding.ASCII.GetString(msg, 17, nameLength);
                _code = Encoding.ASCII.GetString(msg, 17 + nameLength, 3);
                _longitude = BitConverter.ToSingle(msg, 20 + nameLength);
                _latitude = BitConverter.ToSingle(msg, 24 + nameLength);
                _amsl = BitConverter.ToSingle(msg, 28 + nameLength);
                _country = Encoding.ASCII.GetString(msg, 32 + nameLength, 3);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the airport object: {e.Message}", e);
            }
        }
    }
}
