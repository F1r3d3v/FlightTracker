namespace ProjOb.IO
{
    internal class FTRCargoFactory : FTRObjectFactory
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

        public override void Populate(String[] data)
        {
            try
            {
                base.Populate(data);
                _weight = Single.Parse(data[1]);
                _code = data[2];
                _description = data[3];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the cargo object: {e.Message}", e);
            }
        }
    }
}
