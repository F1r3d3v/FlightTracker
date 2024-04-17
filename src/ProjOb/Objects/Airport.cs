using NetworkSourceSimulator;
using ProjOb.Media;
using ProjOb.IO;

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