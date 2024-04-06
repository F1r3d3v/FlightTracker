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
        public string Process(CargoPlane plane) => $"{Name} - An interview with the crew of {plane.Serial}.";
        public string Process(PassengerPlane plane) => $"{Name} - Breaking news! {plane.Model} aircraft loses EASA fails certification after inspection of {plane.Serial}.";
    }
}
