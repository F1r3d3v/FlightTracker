using System.Text.Json.Serialization;

namespace ProjOb
{
    public abstract class Object : IExpandable, IExpandable<string>
    {
        [JsonPropertyOrder(-8)]
        public UInt64 ID { get; set; }

        public abstract void Apply(IComponent component);
        public abstract string Apply(IComponent<string> component);
    }
}