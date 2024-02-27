namespace ProjOb
{
    internal class AirportDTO
    {
        public String? Type { get; set; }
        public UInt64 ID { get; set; }
        public String? Name { get; set; }
        public String? Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public String? Country { get; set; }
    }
}
