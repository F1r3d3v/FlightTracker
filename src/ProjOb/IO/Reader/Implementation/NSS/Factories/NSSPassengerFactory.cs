using System.Text;

namespace ProjOb.IO
{
    internal class NSSPassengerFactory : NSSObjectFactory
    {
        protected String? _name;
        protected UInt64 _age;
        protected String? _phone;
        protected String? _email;
        protected String? _class;
        protected UInt64 _miles;

        public override Passenger Create()
        {
            Passenger passenger = new Passenger();
            passenger.ID = _id;
            passenger.Name = _name;
            passenger.Age = _age;
            passenger.Phone = _phone;
            passenger.Email = _email;
            passenger.Class = _class;
            passenger.Miles = _miles;
            return passenger;
        }

        public override void Populate(Byte[] msg)
        {
            try
            {
                base.Populate(msg);
                UInt16 nameLength = BitConverter.ToUInt16(msg, 15);
                _name = Encoding.UTF8.GetString(msg, 17, nameLength).TrimEnd('\0');
                _age = BitConverter.ToUInt16(msg, 17 + nameLength);
                _phone = Encoding.UTF8.GetString(msg, 19 + nameLength, 12).TrimEnd('\0');
                UInt16 emailLength = BitConverter.ToUInt16(msg, 31 + nameLength);
                _email = Encoding.UTF8.GetString(msg, 33 + nameLength, emailLength).TrimEnd('\0');
                _class = Encoding.UTF8.GetString(msg, 33 + nameLength + emailLength, 1).TrimEnd('\0');
                _miles = BitConverter.ToUInt64(msg, 34 + nameLength + emailLength);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the passenger object: {e.Message}", e);
            }
        }
    }
}
