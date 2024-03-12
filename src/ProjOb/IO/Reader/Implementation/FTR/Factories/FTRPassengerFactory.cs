namespace ProjOb.IO
{
    internal class FTRPassengerFactory : FTRPersonFactory
    {
        protected String? _class;
        protected UInt64 _miles;

        public override Passenger Create()
        {
            Passenger passenger = new Passenger();
            passenger.ID = _id;
            passenger.Name = _name;
            passenger.Age = _age;
            passenger.Phone = _phone;
            passenger.Email = _email;
            passenger.Class = _class;
            passenger.Miles = _miles;
            return passenger;
        }

        public override void Populate(String[] props)
        {
            try
            {
                base.Populate(props);
                _class = props[5];
                _miles = UInt64.Parse(props[6]);
            }
            catch (FormatException e)
            {
                throw new FormatException($"Failed to parse the passenger object: {e.Message}", e);
            }
        }
    }
}
