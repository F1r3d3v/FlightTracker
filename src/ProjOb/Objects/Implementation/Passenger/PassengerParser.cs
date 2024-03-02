namespace ProjOb
{
    internal class PassengerParser : Parser
    {
        public override Object? Parse(String[] props)
        {
            var passengerDTO = new PassengerDTO();

            void del(String[] props)
            {
                passengerDTO.Type = props[0];
                passengerDTO.ID = UInt64.Parse(props[1]);
                passengerDTO.Name = props[2];
                passengerDTO.Age = UInt64.Parse(props[3]);
                passengerDTO.Phone = props[4];
                passengerDTO.Email = props[5];
                passengerDTO.Class = props[6];
                passengerDTO.Miles = UInt64.Parse(props[7]);
            }

            if (!Populate(props, del)) return null;

            return new Passenger(passengerDTO);
        }
    }
}
