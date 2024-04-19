namespace ProjOb.UI
{
    public interface IFlightsGUIDataDecorator
    {
        WorldPosition GetWorldPosition(Flight flight, TimeSpan elapsedTime, TimeSpan flightDuration);
        double GetRotation(Flight flight, WorldPosition currentPos);
    }
}
