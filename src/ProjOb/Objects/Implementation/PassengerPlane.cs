namespace ProjOb
{
    public class PassengerPlane : Plane
    {
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                FirstClassSize = UInt16.Parse(props[4]);
                BusinessClassSize = UInt16.Parse(props[5]);
                EconomyClassSize = UInt16.Parse(props[6]);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }

        public override void Apply(IComponent component)
        {
            component.Process(this);
        }

        public override string ToString() { return "PassengerPlane"; }
    }
}