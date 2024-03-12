using System.Text;

namespace ProjOb.IO
{
    internal class NSSCargoPlaneFactory : NSSObjectFactory
    {
        protected String? _serial;
        protected String? _country;
        protected String? _model;
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

        public override void Populate(Byte[] msg)
        {
            try
            {
                base.Populate(msg);
                _serial = Encoding.ASCII.GetString(msg, 15, 10);
                _country = Encoding.ASCII.GetString(msg, 25, 3);
                UInt16 modelLength = BitConverter.ToUInt16(msg, 28);
                _model = Encoding.ASCII.GetString(msg, 30, modelLength);
                _maxload = BitConverter.ToSingle(msg, 30 + modelLength);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the cargo plane object: {e.Message}", e);
            }
        }
    }
}
