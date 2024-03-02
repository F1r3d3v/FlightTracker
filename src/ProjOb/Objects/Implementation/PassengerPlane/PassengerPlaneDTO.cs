namespace ProjOb
{
    internal class PassengerPlaneDTO
    {
        public String? Type { get; set; }
        public UInt64 ID { get; set; }
        public String? Serial { get; set; }
        public String? Country { get; set; }
        public String? Model { get; set; }
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }
    }
}
