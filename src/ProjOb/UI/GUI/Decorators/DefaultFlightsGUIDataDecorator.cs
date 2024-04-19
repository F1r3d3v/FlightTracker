using Mapsui.Projections;

namespace ProjOb.UI
{
    internal class DefaultFlightsGUIDataDecorator : IFlightsGUIDataDecorator
    {
        private readonly Dictionary<UInt64, WorldPosition> _oldWorldPositions = [];

        public WorldPosition GetWorldPosition(Flight flight, TimeSpan elapsedTime, TimeSpan flightDuration)
        {
            (double longitude, double latitude) departureCords = (flight.Origin?.Longitude ?? 0, flight.Origin?.Latitude ?? 0);
            (double longitude, double latitude) arrivalCords = (flight.Target?.Longitude ?? 0, flight.Target?.Latitude ?? 0);

            double progress = elapsedTime / flightDuration;

            double currentLongitude = MathHelper.Lerp(departureCords.longitude, arrivalCords.longitude, progress);
            double currentLatitude = MathHelper.Lerp(departureCords.latitude, arrivalCords.latitude, progress);

            return new WorldPosition(currentLatitude, currentLongitude);
        }

        public double GetRotation(Flight flight, WorldPosition currentPos)
        {
            (double longitude, double latitude) departureCords = (flight.Origin?.Longitude ?? 0, flight.Origin?.Latitude ?? 0);
            (double x, double y) currentPosition = SphericalMercator.FromLonLat(currentPos.Longitude, currentPos.Latitude);
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

                _oldWorldPositions[flight.ID] = currentPos;
            }

            return MathHelper.CalcRotation(previousPosition, currentPosition);
        }
    }
}
