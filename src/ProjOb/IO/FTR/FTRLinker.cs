namespace ProjOb.IO
{
    internal class FTRLinker : ILinker
    {
        public void Link(Dictionary<String, String[]> records, List<Object> objects)
        {
            var flights = objects.Where(x => x.ToString() == "Flight");
            foreach (Flight flight in flights)
            {
                UInt64 ID;
                String flightID = flight.ID.ToString();

                ID = Convert.ToUInt64(records[flightID][1]);
                flight.Origin = (Airport)objects.First(x => x.ID == ID);

                ID = Convert.ToUInt64(records[flightID][2]);
                flight.Target = (Airport)objects.First(x => x.ID == ID);

                ID = Convert.ToUInt64(records[flightID][8]);
                flight.Plane = (Plane)objects.First(x => x.ID == ID);

                String[] crews = records[flightID][9][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in crews)
                {
                    ID = Convert.ToUInt64(id);
                    flight.Crews.Add((Crew)objects.First(x => x.ID == ID));
                }

                String[] loads = records[flightID][10][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in loads)
                {
                    ID = Convert.ToUInt64(id);
                    flight.Loads.Add((ILoad)objects.First(x => x.ID == ID));
                }
            }
        }
    }
}
