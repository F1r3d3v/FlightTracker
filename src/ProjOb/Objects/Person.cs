using NetworkSourceSimulator;
using System.Text.Json.Serialization;
using ProjOb.IO;

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

        public override void OnContactInfoChanged(object sender, ContactInfoUpdateArgs args)
        {
            Logger.Info($"Object ID {ID}:");
            Logger.Info($"  Phone {Phone} -> {args.PhoneNumber}");
            Logger.Info($"  Email {Email} -> {args.EmailAddress}");
            Phone = args.PhoneNumber;
            Email = args.EmailAddress;
        }
    }
}