using ProjOb.IO;

namespace ProjOb
{
    public class Flight : Object
    {
        [OnlyID] public Airport? Origin { get; set; }
        [OnlyID] public Airport? Target { get; set; }
        public String? TakeoffTime { get; set; }
        public String? LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        [OnlyID] public Plane? Plane { get; set; }
        [OnlyID] public List<Crew> Crews { get; set; } = [];
        [OnlyID] public List<ILoad> Loads { get; set; } = [];

        public override void Apply(IComponent component)
        {
            component.Process(this);
        }
    }
}
