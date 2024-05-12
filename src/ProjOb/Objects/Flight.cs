using NetworkSourceSimulator;
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
        public override T Apply<T>(IComponent<T> component) => component.Process(this)!;

        public override void OnPositionChanged(object sender, PositionUpdateArgs args)
        {
            Logger.Info($"Object ID {ID}:");
            Logger.Info($"  Longitude {Longitude} -> {args.Longitude}");
            Logger.Info($"  Latitude {Latitude} -> {args.Latitude}");
            Logger.Info($"  AMSL {AMSL} -> {args.AMSL}");
            Longitude = args.Longitude;
            Latitude = args.Latitude;
            AMSL = args.AMSL;
        }
    }
}
