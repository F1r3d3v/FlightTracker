namespace ProjOb
{
    public class CargoPlane : Plane
    {
        public Single MaxLoad { get; set; }

        internal CargoPlane(CargoPlaneDTO data)
        {
            Type = data.Type;
            ID = data.ID;
            Serial = data.Serial;
            Country = data.Country;
            Model = data.Model;
            MaxLoad = data.MaxLoad;
        }
    }
}