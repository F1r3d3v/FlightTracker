namespace ProjOb.Media
{
    public class Television : IMedia
    {
        public string Name { get; }

        public Television(string name)
        {
            Name = name;
        }

        public string Process(Airport airport) => $"<An image of {airport.Name} airport>";
    }
}
