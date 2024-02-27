namespace ProjOb
{
    public class Flight : Object
    {
        public UInt64 Origin { get; set; }
        public UInt64 Target { get; set; }
        public String? TakeoffTime { get; set; }
        public String? LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public UInt64[]? CrewIDs { get; set; }
        public UInt64[]? LoadIDs { get; set; }

        internal Flight(FlightDTO data)
        {
            Type = data.Type;
            ID = data.ID;
            Origin = data.Origin;
            Target = data.Target;
            TakeoffTime = data.TakeoffTime;
            LandingTime = data.LandingTime;
            Longitude = data.Longitude;
            Latitude = data.Latitude;
            AMSL = data.AMSL;
            PlaneID = data.PlaneID;
            CrewIDs = data.CrewIDs;
            LoadIDs = data.LoadIDs;
        }
    }
}
