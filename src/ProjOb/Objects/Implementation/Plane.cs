using System.Text.Json.Serialization;

namespace ProjOb
{
    public abstract class Plane : Object
    {
        [JsonPropertyOrder(-4)]
        public String? Serial { get; set; }

        [JsonPropertyOrder(-4)]
        public String? Country { get; set; }

        [JsonPropertyOrder(-4)]
        public String? Model { get; set; }
    }
}