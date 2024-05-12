namespace ProjOb
{
    public class Passenger : Person, ILoad
    {
        public String? Class { get; set; }
        public UInt64 Miles { get; set; }

        public override void Apply(IComponent component) => component.Process(this);
        public override T Apply<T>(IComponent<T> component) => component.Process(this)!;
    }
}