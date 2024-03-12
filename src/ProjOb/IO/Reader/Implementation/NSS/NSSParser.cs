using System.Text;

namespace ProjOb.IO
{
    internal class NSSParser
    {
        private readonly Dictionary<String, Func<NSSObjectFactory>> factories = new()
        {
            { "NCR", () => new NSSCrewFactory() },
            { "NPA", () => new NSSPassengerFactory() },
            { "NCA", () => new NSSCargoFactory() },
            { "NCP", () => new NSSCargoPlaneFactory() },
            { "NPP", () => new NSSPassengerPlaneFactory() },
            { "NAI", () => new NSSAirportFactory() },
            { "NFL", () => new NSSFlightFactory() },
        };

        public Object Parse(Byte[] data)
        {
            String type = Encoding.UTF8.GetString(data, 0, 3).ToUpperInvariant();
            if (factories.TryGetValue(type, out var value))
            {
                NSSObjectFactory factory = value();
                factory.Populate(data);
                return factory.Create();
            }
            else
            {
                throw new ArgumentException($"Unknown type: {type}");
            }
        }
    }
}