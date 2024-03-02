namespace ProjOb
{
    internal class PassengerPlaneParser : Parser
    {
        public override Object? Parse(String[] props)
        {
            var passengerPlaneDTO = new PassengerPlaneDTO();

            void del(String[] props)
            {
                passengerPlaneDTO.Type = props[0];
                passengerPlaneDTO.ID = UInt64.Parse(props[1]);
                passengerPlaneDTO.Serial = props[2];
                passengerPlaneDTO.Country = props[3];
                passengerPlaneDTO.Model = props[4];
                passengerPlaneDTO.FirstClassSize = UInt16.Parse(props[5]);
                passengerPlaneDTO.BusinessClassSize = UInt16.Parse(props[6]);
                passengerPlaneDTO.EconomyClassSize = UInt16.Parse(props[7]);
            }

            if (!Populate(props, del)) return null;

            return new PassengerPlane(passengerPlaneDTO);
        }
    }
}
