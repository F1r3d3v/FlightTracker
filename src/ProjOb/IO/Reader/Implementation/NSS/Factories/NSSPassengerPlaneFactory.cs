using System.Text;

namespace ProjOb.IO
{
    internal class NSSPassengerPlaneFactory : NSSObjectFactory
    {
        protected String? _serial;
        protected String? _country;
        protected String? _model;
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

        public override void Populate(Byte[] msg)
        {
            try
            {
                base.Populate(msg);
                _serial = Encoding.UTF8.GetString(msg, 15, 10).TrimEnd('\0');
                _country = Encoding.UTF8.GetString(msg, 25, 3).TrimEnd('\0');
                UInt16 modelLength = BitConverter.ToUInt16(msg, 28);
                _model = Encoding.UTF8.GetString(msg, 30, modelLength).TrimEnd('\0');
                _firstclasssize = BitConverter.ToUInt16(msg, 30 + modelLength);
                _businessclasssize = BitConverter.ToUInt16(msg, 32 + modelLength);
                _economyclasssize = BitConverter.ToUInt16(msg, 34 + modelLength);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the passenger plane object: {e.Message}", e);
            }
        }
    }
}
