using System.Text;

namespace ProjOb.IO
{
    internal class NSSCrewFactory : NSSObjectFactory
    {
        protected String? _name;
        protected UInt64 _age;
        protected String? _phone;
        protected String? _email;
        protected UInt16 _practice;
        protected String? _role;

        public override Crew Create()
        {
            Crew crew = new Crew();
            crew.ID = _id;
            crew.Name = _name;
            crew.Age = _age;
            crew.Phone = _phone;
            crew.Email = _email;
            crew.Practice = _practice;
            crew.Role = _role;
            return crew;
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
                _practice = BitConverter.ToUInt16(msg, 33 + nameLength + emailLength);
                _role = Encoding.UTF8.GetString(msg, 35 + nameLength + emailLength, 1).TrimEnd('\0');
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the crew object: {e.Message}", e);
            }
        }
    }
}
