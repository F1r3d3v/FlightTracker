using System.Diagnostics.Metrics;

namespace ProjOb
{
    public abstract class Person : Object
    {
        public String? Name { get; set; }
        public UInt64 Age { get; set; }
        public String? Phone { get; set; }
        public String? Email { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                Name = props[1];
                Age = UInt64.Parse(props[2]);
                Phone = props[3];
                Email = props[4];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }
    }
}