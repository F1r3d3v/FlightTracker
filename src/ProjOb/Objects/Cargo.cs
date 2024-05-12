namespace ProjOb
{
    public class Cargo : Object, ILoad
    {
        public Single Weight { get; set; }
        public String? Code { get; set; }
        public String? Description { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override T Apply<T>(IComponent<T> component) => component.Process(this)!;
    }
}
