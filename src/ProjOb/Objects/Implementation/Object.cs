using System.Text.Json.Serialization;

namespace ProjOb
{
    [JsonDerivedType(typeof(Crew), "Crew")]
    [JsonDerivedType(typeof(Passenger), "Passenger")]
    [JsonDerivedType(typeof(Cargo), "Cargo")]
    [JsonDerivedType(typeof(CargoPlane), "CargoPlane")]
    [JsonDerivedType(typeof(PassengerPlane), "PassengerPlane")]
    [JsonDerivedType(typeof(Airport), "Airport")]
    [JsonDerivedType(typeof(Flight), "Flight")]
    public abstract class Object
    {
        [JsonPropertyOrder(-8)]
        public UInt64 ID { get; set; }

        public virtual void Populate(String[] props)
        {
            try
            {
                ID = Convert.ToUInt64(props[0]);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }

        public override string ToString() { return "Object"; }
    }
}