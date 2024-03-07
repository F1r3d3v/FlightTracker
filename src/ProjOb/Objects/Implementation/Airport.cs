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

        public override void Apply(IComponent component)
        {
            component.Process(this);
        }
    }
}