using FlightTrackerGUI;
using ElapsedEventArgs = System.Timers.ElapsedEventArgs;
using Timer = System.Timers.Timer;

namespace ProjOb.UI
{
    public static class FlightTracker
    {
        public static void RunGUI(Database db)
        {
            Task t = Task.Run(() => Runner.Run());
            FlightsGUIDataAdapter wrapper = new FlightsGUIDataAdapter(db);

            Timer timer = new(1000);
            timer.Elapsed += (object? sender, ElapsedEventArgs e) =>
            {
                FlightsGUIData data = wrapper.ConvertToFlightsGUIData(e.SignalTime);
                Runner.UpdateGUI(data);
                Console.WriteLine($"Current Time: {e.SignalTime:HH:mm:ss}");
            };
            timer.AutoReset = true;
            timer.Enabled = true;

            t.Wait();
            timer.Enabled = false;
        }
    }
}
