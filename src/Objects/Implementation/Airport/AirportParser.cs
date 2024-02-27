namespace ProjOb
{
    internal class AirportParser : Parser
    {
        public override Object? Parse(String[] props)
        {
            var airportDTO = new AirportDTO();

            void del(String[] props)
            {
                airportDTO.Type = props[0];
                airportDTO.ID = UInt64.Parse(props[1]);
                airportDTO.Name = props[2];
                airportDTO.Code = props[3];
                airportDTO.Longitude = Single.Parse(props[4]);
                airportDTO.Latitude = Single.Parse(props[5]);
                airportDTO.AMSL = Single.Parse(props[6]);
                airportDTO.Country = props[7];
            }

            if (!Populate(props, del)) return null;

            return new Airport(airportDTO);
        }
    }
}
