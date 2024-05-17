using NetworkSourceSimulator;
using ProjOb.IO;
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

        public override void OnContactInfoChanged(object sender, ContactInfoUpdateArgs args)
        {
            Phone = args.PhoneNumber;
            Email = args.EmailAddress;
            Logger.InfoAsync($"Object ID {ID}:");
            Logger.InfoAsync($"  Phone {Phone} -> {args.PhoneNumber}");
            Logger.InfoAsync($"  Email {Email} -> {args.EmailAddress}");
        }
    }
}