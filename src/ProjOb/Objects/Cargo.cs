namespace ProjOb
{
    public class Cargo : Object, ILoad
    {
        public Single Weight { get; set; }
        public String? Code { get; set; }
        public String? Description { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override string Apply(IComponent<string> component) => component.Process(this)!;
    }
}
