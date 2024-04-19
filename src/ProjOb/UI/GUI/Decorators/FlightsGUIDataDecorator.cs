namespace ProjOb.UI
{
    internal abstract class FlightsGUIDataDecorator : IFlightsGUIDataDecorator
    {
        protected readonly IFlightsGUIDataDecorator _flightGUIDataDecorator;

        public FlightsGUIDataDecorator(IFlightsGUIDataDecorator flightGUIDataDecorator)
        {
            _flightGUIDataDecorator = flightGUIDataDecorator;
        }

        public virtual WorldPosition GetWorldPosition(Flight flight, TimeSpan elapsedTime, TimeSpan flightDuration)
        {
            return _flightGUIDataDecorator.GetWorldPosition(flight, elapsedTime, flightDuration);
        }

        public virtual double GetRotation(Flight flight, WorldPosition currentPos)
        {
            return _flightGUIDataDecorator.GetRotation(flight, currentPos);
        }
    }
}
