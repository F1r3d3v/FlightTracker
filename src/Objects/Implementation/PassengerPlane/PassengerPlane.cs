namespace ProjOb
{
    public class PassengerPlane : Plane
    {
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        internal PassengerPlane(PassengerPlaneDTO data)
        {
            Type = data.Type;
            ID = data.ID;
            Serial = data.Serial;
            Country = data.Country;
            Model = data.Model;
            FirstClassSize = data.FirstClassSize;
            BusinessClassSize = data.BusinessClassSize;
            EconomyClassSize = data.EconomyClassSize;
        }
    }
}