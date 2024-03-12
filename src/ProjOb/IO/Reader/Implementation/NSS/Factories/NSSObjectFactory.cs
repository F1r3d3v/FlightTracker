using NetworkSourceSimulator;

namespace ProjOb.IO
{
    internal abstract class NSSObjectFactory
    {
        protected UInt64 _id;

        public abstract Object Create();

        public virtual void Populate(Byte[] msg)
        {
            _id = BitConverter.ToUInt64(msg, 7);
        }
    }
}
