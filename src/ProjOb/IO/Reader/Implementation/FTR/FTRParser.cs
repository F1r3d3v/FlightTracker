namespace ProjOb.IO
{
    internal class FTRParser
    {
        private readonly Dictionary<string, Func<FTRObjectFactory>> factories = new()
        {
            { "C", () => new FTRCrewFactory() },
            { "P", () => new FTRPassengerFactory() },
            { "CA", () => new FTRCargoFactory() },
            { "CP", () => new FTRCargoPlaneFactory() },
            { "PP", () => new FTRPassengerPlaneFactory() },
            { "AI", () => new FTRAirportFactory() },
            { "FL", () => new FTRFlightFactory() },
        };

        public Object Parse(String[] data)
        {
            if (factories.TryGetValue(data[0].ToUpperInvariant(), out var value))
            {
                FTRObjectFactory factory = value();
                factory.Populate(data[1..]);
                return factory.Create();
            }
            else
            {
                throw new ArgumentException($"Unknown type: {data[0]}");
            }
        }
    }
}