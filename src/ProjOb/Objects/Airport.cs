using ProjOb.Media;

namespace ProjOb
{
    public class Airport : Object, IReportable
    {
        public String? Name { get; set; }
        public String? Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public String? Country { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override string Apply(IComponent<string> component) => component.Process(this)!;
    }
}