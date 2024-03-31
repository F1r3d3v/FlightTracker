namespace ProjOb.Media
{
    public class Radio : IMedia
    {
        public string Name { get; }

        public Radio(string name)
        {
            Name = name;
        }

        public string Process(Airport airport) => $"Reporting for {Name}, Ladies and gentelmen, we are at the {airport.Name} airport.";
    }
}
