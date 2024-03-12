namespace ProjOb
{
    public class Passenger : Person, ILoad
    {
        public String? Class { get; set; }
        public UInt64 Miles { get; set; }

        public override void Apply(IComponent component)
        {
            component.Process(this);
        }
    }
}