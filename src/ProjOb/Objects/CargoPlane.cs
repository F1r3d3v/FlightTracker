using ProjOb.Media;
using System.Data;

namespace ProjOb
{
    public class CargoPlane : Plane, IReportable
    {
        public Single MaxLoad { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override T Apply<T>(IComponent<T> component) => component.Process(this)!;
    }
}