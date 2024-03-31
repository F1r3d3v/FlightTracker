namespace ProjOb
{
    public class Passenger : Person, ILoad
    {
        public String? Class { get; set; }
        public UInt64 Miles { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override string Apply(IComponent<string> component) => component.Process(this)!;
    }
}