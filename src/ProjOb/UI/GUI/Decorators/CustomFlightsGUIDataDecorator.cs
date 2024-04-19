using Mapsui.Projections;

namespace ProjOb.UI
{
    internal class CustomFlightsGUIDataDecorator : FlightsGUIDataDecorator
    {
        private readonly Dictionary<UInt64, WorldPosition> _oldWorldPositions = [];

        public CustomFlightsGUIDataDecorator(IFlightsGUIDataDecorator flightGUIDataDecorator) : base(flightGUIDataDecorator) { }

        public override WorldPosition GetWorldPosition(Flight flight, TimeSpan elapsedTime, TimeSpan flightDuration)
        {
            (double longitude, double latitude) arrivalCords = (flight.Target?.Longitude ?? 0, flight.Target?.Latitude ?? 0);

            float speedLongitude = (float)((arrivalCords.longitude - flight.Longitude) / (flightDuration - elapsedTime).TotalSeconds);
            float speedLatitude = (float)((arrivalCords.latitude - flight.Latitude) / (flightDuration - elapsedTime).TotalSeconds);

            double currentLongitude = flight.Longitude + speedLongitude;
            double currentLatitude = flight.Latitude + speedLatitude;

            return new WorldPosition(currentLatitude, currentLongitude);
        }

        public override double GetRotation(Flight flight, WorldPosition currentPos)
        {
            (double x, double y) currentPosition = SphericalMercator.FromLonLat(currentPos.Longitude, currentPos.Latitude);
            (double x, double y) previousPosition;

            lock (_oldWorldPositions)
            {
                if (!_oldWorldPositions.ContainsKey(flight.ID))
                {
                    previousPosition = SphericalMercator.FromLonLat(flight.Longitude, flight.Latitude);
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
