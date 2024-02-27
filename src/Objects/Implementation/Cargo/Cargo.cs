namespace ProjOb
{
    public class Cargo : Object
    {
        public Single Weight { get; set; }
        public String? Code { get; set; }
        public String? Description { get; set; }

        internal Cargo(CargoDTO data)
        {
            Type = data.Type;
            ID = data.ID;
            Weight = data.Weight;
            Code = data.Code;
            Description = data.Description;
        }
    }
}
