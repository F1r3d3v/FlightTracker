using System.Diagnostics.Metrics;

namespace ProjOb
{
    public class Crew : Person
    {
        public UInt16 Practice { get; set; }
        public String? Role { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                Practice = UInt16.Parse(props[5]);
                Role = props[6];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }

        public override void Apply(IComponent component)
        {
            component.Process(this);
        }

        public override string ToString() { return "Crew"; }
    }
}