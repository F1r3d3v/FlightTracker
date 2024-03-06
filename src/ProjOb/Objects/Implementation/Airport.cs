namespace ProjOb
{
    public class Airport : Object
    {
        public String? Name { get; set; }
        public String? Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public String? Country { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                Name = props[1];
                Code = props[2];
                Longitude = Single.Parse(props[3]);
                Latitude = Single.Parse(props[4]);
                AMSL = Single.Parse(props[5]);
                Country = props[6];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }

        public override void Apply(IComponent component)
        {
            component.Process(this);
        }

        public override string ToString() { return "Airport";  }
    }
}