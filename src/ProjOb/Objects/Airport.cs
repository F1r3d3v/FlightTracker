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
        public override T Apply<T>(IComponent<T> component) => component.Process(this)!;

        public override void OnPositionChanged(object sender, PositionUpdateArgs args)
        {
            Longitude = args.Longitude;
            Latitude = args.Latitude;
            AMSL = args.AMSL;
            Logger.InfoAsync($"Object ID {ID}:");
            Logger.InfoAsync($"  Longitude {Longitude} -> {args.Longitude}");
            Logger.InfoAsync($"  Latitude {Latitude} -> {args.Latitude}");
            Logger.InfoAsync($"  AMSL {AMSL} -> {args.AMSL}");
        }
    }
}