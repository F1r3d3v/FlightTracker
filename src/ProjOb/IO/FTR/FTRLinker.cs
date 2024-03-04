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
                String originID = records[flightID][2];
                String targetID = records[flightID][3];
                String planeID = records[flightID][9];

                ID = UInt64.Parse(originID);
                flight.Origin = (Airport)objects.First(x => x.ID == ID);

                ID = UInt64.Parse(targetID);
                flight.Target = (Airport)objects.First(x => x.ID == ID);

                ID = UInt64.Parse(planeID);
                flight.Plane = (Plane)objects.First(x => x.ID == ID);

                String[] crews = records[flightID][10][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in crews)
                {
                    ID = UInt64.Parse(id);
                    flight.Crews.Add((Crew)objects.First(x => x.ID == ID));
                }

                String[] loads = records[flightID][11][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in loads)
                {
                    ID = UInt64.Parse(id);
                    flight.Loads.Add((ILoad)objects.First(x => x.ID == ID));
                }
            }
        }
    }
}
