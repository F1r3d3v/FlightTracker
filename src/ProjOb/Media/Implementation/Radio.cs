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
        public string Process(CargoPlane plane) => $"Reporting for {Name}, Ladies and gentelmen, we are seeing the {plane.Serial} aircraft fly above us.";
        public string Process(PassengerPlane plane) => $"Reporting for {Name}, Ladies and gentelmen, we’ve just witnessed {plane.Serial} take off.";
    }
}
