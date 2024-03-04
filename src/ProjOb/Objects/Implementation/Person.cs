using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace ProjOb
{
    public abstract class Person : Object
    {
        [JsonPropertyOrder(-4)]
        public String? Name { get; set; }

        [JsonPropertyOrder(-4)]
        public UInt64 Age { get; set; }

        [JsonPropertyOrder(-4)]
        public String? Phone { get; set; }

        [JsonPropertyOrder(-4)]
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

        public override string ToString() { return "Person"; }
    }
}