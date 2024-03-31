
namespace ProjOb.UI
{
    public interface IFlightTrackerAdapter
    {
        FlightsGUIData ConvertToFlightsGUIData(DateTime currentTime);
    }
}
