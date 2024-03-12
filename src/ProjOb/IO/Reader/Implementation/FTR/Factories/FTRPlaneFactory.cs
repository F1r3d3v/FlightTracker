namespace ProjOb.IO
{
    internal abstract class FTRPlaneFactory : FTRObjectFactory
    {
        protected String? _serial;
        protected String? _country;
        protected String? _model;
        public override void Populate(String[] data)
        {
            base.Populate(data);
            _serial = data[1];
            _country = data[2];
            _model = data[3];
        }
    }
}
