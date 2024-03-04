namespace ProjOb.IO
{
    internal class FTRLinker : ILinker
    {
        public void Link(Dictionary<String, String[]> records, Database database)
        {
            foreach (Flight flight in database.Flights)
            {
                UInt64 ID;
                String flightID = flight.ID.ToString();
                UInt64 originID = UInt64.Parse(records[flightID][2]);
                UInt64 targetID = UInt64.Parse(records[flightID][3]);
                UInt64 planeID = UInt64.Parse(records[flightID][9]);

                flight.Origin = database.Airports.First(x => x.ID == originID);

                flight.Target = database.Airports.First(x => x.ID == targetID);

                if (database.CargoPlanes.Any(x => x.ID == planeID))
                    flight.Plane = database.CargoPlanes.First(x => x.ID == planeID);
                else
                    flight.Plane = database.PassengerPlanes.First(x => x.ID == planeID);

                String[] crews = records[flightID][10][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in crews)
                {
                    ID = UInt64.Parse(id);
                    flight.Crews.Add(database.Crews.First(x => x.ID == ID));
                }

                String[] loads = records[flightID][11][1..^1].Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (String id in loads)
                {
                    ID = UInt64.Parse(id);
                    ILoad load;

                    if (database.Cargos.Any(x => x.ID == ID))
                        load = database.Cargos.First(x => x.ID == ID);
                    else
                        load = database.Passengers.First(x => x.ID == ID);

                    flight.Loads.Add(load);
                }
            }
        }
    }
}
