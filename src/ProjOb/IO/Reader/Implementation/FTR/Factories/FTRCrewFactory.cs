namespace ProjOb.IO
{
    internal class FTRCrewFactory : FTRPersonFactory
    {
        protected UInt16 _practice;
        protected String? _role;

        public override Crew Create()
        {
            Crew crew = new Crew();
            crew.ID = _id;
            crew.Name = _name;
            crew.Age = _age;
            crew.Phone = _phone;
            crew.Email = _email;
            crew.Practice = _practice;
            crew.Role = _role;
            return crew;
        }

        public override void Populate(String[] data)
        {
            try
            {
                base.Populate(data);
                _practice = UInt16.Parse(data[5]);
                _role = data[6];
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the crew object: {e.Message}", e);
            }
        }
    }
}
