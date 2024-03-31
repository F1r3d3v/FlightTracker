namespace ProjOb
{
    public class Crew : Person
    {
        public UInt16 Practice { get; set; }
        public String? Role { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override string Apply(IComponent<string> component) => component.Process(this)!;
    }
}