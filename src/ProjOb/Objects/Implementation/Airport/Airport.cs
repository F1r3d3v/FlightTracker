namespace ProjOb
{
    public class Airport : Object
    {
        public String? Name { get; set; }
        public String? Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public String? Country { get; set; }

        internal Airport(AirportDTO data)
        {
            Type = data.Type;
            ID = data.ID;
            Name = data.Name;
            Code = data.Code;
            Longitude = data.Longitude;
            Latitude = data.Latitude;
            AMSL = data.AMSL;
            Country = data.Country;
        }
    }
}