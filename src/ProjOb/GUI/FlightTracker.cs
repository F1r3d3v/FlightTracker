using FlightTrackerGUI;
using ProjOb.IO;
using Timer = System.Timers.Timer;
using ElapsedEventArgs = System.Timers.ElapsedEventArgs;

namespace ProjOb.GUI;

public static class FlightTracker
{
    public static void RunGUI(ILoader loader, Database db)
    {
        Task.Run(() => loader.LoadToDatabase(db));
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
    }
}
