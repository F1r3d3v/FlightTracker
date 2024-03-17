using Mapsui.Projections;
using ProjOb.Wrappers;

namespace ProjOb.GUI
{
    internal class FlightsGUIDataAdapter : IFlightTrackerAdapter
    {
        private readonly Database _db;
        private readonly Dictionary<UInt64, WorldPosition> _oldWorldPositions = [];

        public FlightsGUIDataAdapter(Database db)
        {
            _db = db;
        }

        public FlightsGUIData ConvertToFlightsGUIData(DateTime currentTime)
        {
            List<FlightGUI> list = new List<FlightGUI>();
            lock (_db.Flights.Values)
            {
                foreach (Flight flight in _db.Flights.Values)
                {
                    (double longitude, double latitude) departureCords = (flight.Origin?.Longitude ?? 0, flight.Origin?.Latitude ?? 0);
                    (double longitude, double latitude) arrivalCords = (flight.Target?.Longitude ?? 0, flight.Target?.Latitude ?? 0);

                    TimeSpan LandingTime = DateTime.Parse(flight.LandingTime!).TimeOfDay;
                    TimeSpan TakeoffTime = DateTime.Parse(flight.TakeoffTime!).TimeOfDay;

                    TimeSpan flightDuration = LandingTime - TakeoffTime;
                    TimeSpan elapsedTime = currentTime.TimeOfDay - TakeoffTime;

                    if (elapsedTime.Ticks <= 0 || flightDuration < elapsedTime) continue;

                    double progress = elapsedTime / flightDuration;

                    double currentLongitude = Lerp(departureCords.longitude, arrivalCords.longitude, progress);
                    double currentLatitude = Lerp(departureCords.latitude, arrivalCords.latitude, progress);

                    (double x, double y) currentPosition = SphericalMercator.FromLonLat(currentLongitude, currentLatitude);
                    (double x, double y) previousPosition;

                    lock (_oldWorldPositions)
                    {
                        if (!_oldWorldPositions.ContainsKey(flight.ID))
                        {
                            previousPosition = SphericalMercator.FromLonLat(departureCords.longitude, departureCords.latitude);
                        }
                        else
                        {
                            previousPosition = SphericalMercator.FromLonLat(_oldWorldPositions[flight.ID].Longitude, _oldWorldPositions[flight.ID].Latitude);
                        }

                        _oldWorldPositions[flight.ID] = new WorldPosition(currentLatitude, currentLongitude);
                    }

                    list.Add(new FlightGUI
                    {
                        ID = flight.ID,
                        WorldPosition = new WorldPosition(currentLatitude, currentLongitude),
                        MapCoordRotation = CalcRotation(previousPosition, currentPosition)
                    });
                }
            }

            return new FlightsGUIData(list);
        }

        private double Lerp(double start, double end, double progress)
        {
            return start + (end - start) * progress;
        }

        private double CalcRotation((double, double) lastPos, (double, double) currPos)
        {
            return Math.Atan2(currPos.Item2 - lastPos.Item2, lastPos.Item1 - currPos.Item1) - Math.PI / 2.0;
        }
    }
}
