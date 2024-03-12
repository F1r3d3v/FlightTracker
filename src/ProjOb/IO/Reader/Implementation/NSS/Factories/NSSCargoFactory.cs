using System.Text;

namespace ProjOb.IO
{
    internal class NSSCargoFactory : NSSObjectFactory
    {
        protected Single _weight;
        protected String? _code;
        protected String? _description;

        public override Cargo Create()
        {
            Cargo cargo = new Cargo();
            cargo.ID = _id;
            cargo.Weight = _weight;
            cargo.Code = _code;
            cargo.Description = _description;
            return cargo;
        }

        public override void Populate(Byte[] msg)
        {
            try
            {
                base.Populate(msg);
                _weight = BitConverter.ToSingle(msg, 15);
                _code = Encoding.ASCII.GetString(msg, 19, 6);
                UInt16 descLength = BitConverter.ToUInt16(msg, 25);
                _description = Encoding.ASCII.GetString(msg, 27, descLength);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the cargo object: {e.Message}", e);
            }
        }
    }
}
