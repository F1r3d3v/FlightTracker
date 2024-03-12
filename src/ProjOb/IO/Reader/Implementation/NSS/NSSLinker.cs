using NetworkSourceSimulator;

namespace ProjOb.IO
{
    internal class NSSLinker
    {
        public void Link(Byte[] msg, Database database)
        {
            UInt64 flightID = BitConverter.ToUInt64(msg, 7);
            if (database.Flights.TryGetValue(flightID, out Flight? flight))
            {
                UInt64 ID;

                UInt64 originID = BitConverter.ToUInt64(msg, 15);
                UInt64 targetID = BitConverter.ToUInt64(msg, 23);
                UInt64 planeID = BitConverter.ToUInt64(msg, 47);

                flight.Origin = database.Airports[originID];
                flight.Target = database.Airports[targetID];

                UInt16 crewCount = BitConverter.ToUInt16(msg, 55);
                for (int i = 0; i < crewCount; i++)
                {
                    ID = BitConverter.ToUInt64(msg, 57 + 8 * i);
                    flight.Crews.Add(database.Crews[ID]);
                }

                UInt16 loadCount = BitConverter.ToUInt16(msg, 57 + 8 * crewCount);
                if (database.CargoPlanes.ContainsKey(planeID))
                {
                    flight.Plane = database.CargoPlanes[planeID];
                    for (int i = 0; i < loadCount; i++)
                    {
                        ID = BitConverter.ToUInt64(msg, 59 + 8 * crewCount + 8 * i);
                        flight.Loads.Add(database.Cargos[ID]);
                    }
                }
                else
                {
                    flight.Plane = database.PassengerPlanes[planeID];
                    for (int i = 0; i < loadCount; i++)
                    {
                        ID = BitConverter.ToUInt64(msg, 59 + 8 * crewCount + 8 * i);
                        flight.Loads.Add(database.Passengers[ID]);
                    }
                }
            }
        }
    }
}
