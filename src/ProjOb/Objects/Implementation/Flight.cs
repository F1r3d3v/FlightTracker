namespace ProjOb
{
    public class Flight : Object
    {
        public Airport? Origin { get; set; }
        public Airport? Target { get; set; }
        public String? TakeoffTime { get; set; }
        public String? LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public Plane? Plane { get; set; }
        public List<Crew> Crews { get; set; }
        public List<ILoad> Loads { get; set; }

        public Flight()
        {
            Crews = new List<Crew>();
            Loads = new List<ILoad>();
        }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                TakeoffTime = props[3];
                LandingTime = props[4];
                Longitude = Single.Parse(props[5]);
                Latitude = Single.Parse(props[6]);
                AMSL = Single.Parse(props[7]);
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

        public override string ToString() { return "Flight"; }
    }
}
