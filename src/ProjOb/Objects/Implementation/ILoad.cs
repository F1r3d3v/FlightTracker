using System.Text.Json.Serialization;

namespace ProjOb
{
    [JsonDerivedType(typeof(Cargo), "Cargo")]
    [JsonDerivedType(typeof(Passenger), "Passenger")]
    public interface ILoad
    {
    }
}
