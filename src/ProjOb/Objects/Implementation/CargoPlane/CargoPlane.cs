using System.Data;

namespace ProjOb
{
    public class CargoPlane : Plane
    {
        public Single MaxLoad { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                MaxLoad = Single.Parse(props[4]);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }

        public override string ToString() { return "CargoPlane"; }
    }
}