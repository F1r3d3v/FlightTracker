namespace ProjOb.Media
{
    public class Newspaper : IMedia
    {
        public string Name { get; }

        public Newspaper(string name)
        {
            Name = name;
        }

        public string Process(Airport airport) => $"{Name} - A report from the {airport.Name} airport, {airport.Country}.";
    }
}
