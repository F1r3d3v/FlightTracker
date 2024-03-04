using System.Security.Claims;

namespace ProjOb
{
    public class Cargo : Object, ILoad
    {
        public Single Weight { get; set; }
        public String? Code { get; set; }
        public String? Description { get; set; }

        public override void Populate(String[] props)
        {
            base.Populate(props);
            try
            {
                Weight = Single.Parse(props[1]);
                Code = props[2];
                Description = props[3];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the object: {e.Message}", e);
            }
        }

        public override string ToString() { return "Cargo"; }
    }
}
