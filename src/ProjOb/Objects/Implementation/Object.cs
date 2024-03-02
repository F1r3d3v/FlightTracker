using System.Text.Json.Serialization;

namespace ProjOb
{
    [JsonDerivedType(typeof(Crew))]
    [JsonDerivedType(typeof(Passenger))]
    [JsonDerivedType(typeof(Cargo))]
    [JsonDerivedType(typeof(CargoPlane))]
    [JsonDerivedType(typeof(PassengerPlane))]
    [JsonDerivedType(typeof(Airport))]
    [JsonDerivedType(typeof(Flight))]
    public abstract class Object
    {
        public String? Type { get; set; }
        public UInt64 ID { get; set; }
    }
}