namespace ProjOb
{
    public abstract class Plane : Object
    {
        public String? Serial { get; set; }
        public String? Country { get; set; }
        public String? Model { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                Serial = props[1];
                Country = props[2];
                Model = props[3];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }
    }
}