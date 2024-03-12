namespace ProjOb.IO
{
    internal class FTRLinker
    {
        public void Link(Dictionary<String, String[]> records, Database database)
        {
            foreach (Flight flight in database.Flights.Values)
            {
                UInt64 ID;
                String flightID = flight.ID.ToString();
                UInt64 originID = UInt64.Parse(records[flightID][2]);
                UInt64 targetID = UInt64.Parse(records[flightID][3]);
                UInt64 planeID = UInt64.Parse(records[flightID][9]);

                flight.Origin = database.Airports[originID];

                flight.Target = database.Airports[targetID];

                if (database.CargoPlanes.ContainsKey(planeID))
                    flight.Plane = database.CargoPlanes[planeID];
                else
                    flight.Plane = database.PassengerPlanes[planeID];

                String[] crews = records[flightID][10][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in crews)
                {
                    ID = UInt64.Parse(id);
                    flight.Crews.Add(database.Crews[ID]);
                }

                String[] loads = records[flightID][11][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in loads)
                {
                    ID = UInt64.Parse(id);
                    ILoad load;

                    if (database.Cargos.ContainsKey(ID))
                        load = database.Cargos[ID];
                    else
                        load = database.Passengers[ID];

                    flight.Loads.Add(load);
                }
            }
        }
    }
}
