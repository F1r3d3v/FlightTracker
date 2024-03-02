namespace ProjOb
{
    internal class FlightParser : Parser
    {
        public override Object? Parse(String[] props)
        {
            var flightDTO = new FlightDTO();

            void del(String[] props)
            {
                flightDTO.Type = props[0];
                flightDTO.ID = UInt64.Parse(props[1]);
                flightDTO.Origin = UInt64.Parse(props[2]);
                flightDTO.Target = UInt64.Parse(props[3]);
                flightDTO.TakeoffTime = props[4];
                flightDTO.LandingTime = props[5];
                flightDTO.Longitude = Single.Parse(props[6]);
                flightDTO.Latitude = Single.Parse(props[7]);
                flightDTO.AMSL = Single.Parse(props[8]);
                flightDTO.PlaneID = UInt64.Parse(props[9]);

                String[] crews = props[10][1..^1].Split(';', StringSplitOptions.TrimEntries);
                flightDTO.CrewIDs = new UInt64[crews.Length];
                for (int i = 0; i < crews.Length; ++i)
                {
                    flightDTO.CrewIDs[i] = UInt64.Parse(crews[i]);
                }

                String[] loads = props[11][1..^1].Split(';', StringSplitOptions.TrimEntries);
                flightDTO.LoadIDs = new UInt64[loads.Length];
                for (int i = 0; i < loads.Length; ++i)
                {
                    flightDTO.LoadIDs[i] = UInt64.Parse(loads[i]);
                }
            }

            if (!Populate(props, del)) return null;

            return new Flight(flightDTO);
        }
    }
}
