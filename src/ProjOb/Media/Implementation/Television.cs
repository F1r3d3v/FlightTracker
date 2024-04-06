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
        public string Process(CargoPlane plane) => $"<An image of {plane.Serial} cargo plane>";
        public string Process(PassengerPlane plane) => $"<An image of {plane.Serial} passenger plane>";
    }
}
