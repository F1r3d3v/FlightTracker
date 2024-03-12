using System.Diagnostics.Metrics;

namespace ProjOb
{
    public class Crew : Person
    {
        public UInt16 Practice { get; set; }
        public String? Role { get; set; }

        public override void Apply(IComponent component)
        {
            component.Process(this);
        }
    }
}