namespace ProjOb.IO
{
    internal abstract class FTRPersonFactory : FTRObjectFactory
    {
        protected String? _name;
        protected UInt64 _age;
        protected String? _phone;
        protected String? _email;

        public override void Populate(String[] data)
        {
            base.Populate(data);
            _name = data[1];
            _age = UInt64.Parse(data[2]);
            _phone = data[3];
            _email = data[4];
        }
    }
}
