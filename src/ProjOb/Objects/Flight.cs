using ProjOb.IO;

namespace ProjOb
{
    public class Flight : Object
    {
        [JsonOnlyID] public Airport? Origin { get; set; }
        [JsonOnlyID] public Airport? Target { get; set; }
        public TimeSpan TakeoffTime { get; set; }
        public TimeSpan LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        [JsonOnlyID] public Plane? Plane { get; set; }
        [JsonOnlyID] public List<Crew> Crews { get; set; } = [];
        [JsonOnlyID] public List<ILoad> Loads { get; set; } = [];

        public override void Apply(IComponent component) => component.Process(this);
        public override string Apply(IComponent<string> component) => component.Process(this)!;
    }
}
