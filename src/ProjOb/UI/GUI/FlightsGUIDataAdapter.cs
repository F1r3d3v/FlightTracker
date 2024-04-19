namespace ProjOb.UI
{
    internal class FlightsGUIDataAdapter : IFlightTrackerAdapter
    {
        private readonly IFlightsGUIDataDecorator _decorator;

        public FlightsGUIDataAdapter(IFlightsGUIDataDecorator decorator)
        {
            _decorator = decorator;
        }

        public FlightsGUIData ConvertToFlightsGUIData(DateTime currentTime, Database db)
        {
            List<FlightGUI> list = new List<FlightGUI>();

            lock (db.Flights.Values)
            {
                foreach (Flight flight in db.Flights.Values)
                {
                    (TimeSpan elapsedTime, TimeSpan flightDuration) = CalculateElapsedAndTotalTime(flight, currentTime);

                    if (elapsedTime.Ticks > 0 && flightDuration >= elapsedTime)
                    {
                        WorldPosition currentPosition = _decorator.GetWorldPosition(flight, elapsedTime, flightDuration);
                        double currentRotation = _decorator.GetRotation(flight, currentPosition);

                        lock (flight)
                        {
                            flight.Longitude = (float)currentPosition.Longitude;
                            flight.Latitude = (float)currentPosition.Latitude;
                        }

                        list.Add(new FlightGUI
                        {
                            ID = flight.ID,
                            WorldPosition = currentPosition,
                            MapCoordRotation = currentRotation
                        });
                    }
                }
            }

            return new FlightsGUIData(list);
        }

        private (TimeSpan elapsedTime, TimeSpan flightDuration) CalculateElapsedAndTotalTime(Flight flight, DateTime currentTime)
        {
            TimeSpan flightDuration;
            TimeSpan elapsedTime;
            if (flight.LandingTime >= flight.TakeoffTime)
            {
                flightDuration = flight.LandingTime - flight.TakeoffTime;
                elapsedTime = currentTime.TimeOfDay - flight.TakeoffTime;
            }
            else
            {
                flightDuration = TimeSpan.FromDays(1) - flight.TakeoffTime + flight.LandingTime;
                elapsedTime = currentTime.TimeOfDay >= flight.TakeoffTime
                    ? currentTime.TimeOfDay - flight.TakeoffTime
                    : flightDuration - (flight.LandingTime - currentTime.TimeOfDay);
            }

            return (elapsedTime, flightDuration);
        }
    }
}
