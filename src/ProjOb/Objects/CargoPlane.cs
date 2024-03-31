using System.Data;

namespace ProjOb
{
    public class CargoPlane : Plane
    {
        public Single MaxLoad { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override string Apply(IComponent<string> component) => component.Process(this)!;
    }
}