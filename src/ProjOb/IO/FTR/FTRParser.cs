namespace ProjOb.IO
{
    internal class FTRParser : IParser
    {
        private readonly Dictionary<string, Func<Object>> factories = new()
        {
            { "C", () => new Crew() },
            { "P", () => new Passenger() },
            { "CA", () => new Cargo() },
            { "CP", () => new CargoPlane() },
            { "PP", () => new PassengerPlane() },
            { "AI", () => new Airport() },
            { "FL", () => new Flight() },
        };

        public void Parse(Dictionary<String, String[]> records, List<Object> objects)
        {
            foreach (var rec in records)
            {
                String[] data = rec.Value;
                if (factories.TryGetValue(data[0].ToUpperInvariant(), out var value))
                {
                    Object obj = value();
                    obj.Populate(data[1..]);
                    objects.Add(obj);
                }
                else
                {
                    throw new ArgumentException($"Unknown type: {data[0]}");
                }
            }
        }
    }
}