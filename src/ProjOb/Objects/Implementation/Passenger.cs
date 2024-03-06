using System.Data;

namespace ProjOb
{
    public class Passenger : Person, ILoad
    {
        public String? Class { get; set; }
        public UInt64 Miles { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                Class = props[5];
                Miles = UInt64.Parse(props[6]);
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

        public override string ToString() { return "Passenger"; }
    }
}