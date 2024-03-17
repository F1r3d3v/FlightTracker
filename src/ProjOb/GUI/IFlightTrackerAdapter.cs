
namespace ProjOb.Wrappers
{
    public interface IFlightTrackerAdapter
    {
        FlightsGUIData ConvertToFlightsGUIData(DateTime currentTime);
    }
}
