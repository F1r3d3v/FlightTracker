namespace ProjOb
{
    public static class ObjectFactory
    {
        private static readonly Dictionary<string, Func<Parser>> factories = new()
        {
            { "C", () => new CrewParser() },
            { "P", () => new PassengerParser() },
            { "CA", () => new CargoParser() },
            { "CP", () => new CargoPlaneParser() },
            { "PP", () => new PassengerPlaneParser() },
            { "AI", () => new AirportParser() },
            { "FL", () => new FlightParser() },
        };

        public static Object? Create(String[] props)
        {
            if (factories.TryGetValue(props[0].ToUpperInvariant(), out var value))
            {
                return value().Parse(props);
            }
            else
            {
                Console.WriteLine($"Unknown type: {props[0]}");
                return null;
            }
        }
    }
}