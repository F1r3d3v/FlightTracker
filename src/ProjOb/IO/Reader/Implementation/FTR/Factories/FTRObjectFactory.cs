namespace ProjOb.IO
{
    internal abstract class FTRObjectFactory
    {
        protected UInt64 _id;

        public abstract Object Create();

        public virtual void Populate(String[] data)
        {
            _id = Convert.ToUInt64(data[0]);
        }
    }
}
