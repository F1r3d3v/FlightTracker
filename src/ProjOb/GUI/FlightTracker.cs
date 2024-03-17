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

        DateTime time = DateTime.Now;
        Timer aTimer = new(1000);
        aTimer.Elapsed += (object? sender, ElapsedEventArgs e) =>
        {
            time = time.AddMinutes(2);
            FlightsGUIData data = wrapper.ConvertToFlightsGUIData(time);
            Runner.UpdateGUI(data);
            Console.WriteLine($"Current Time: {time.ToString()}");
        };
        aTimer.AutoReset = true;
        aTimer.Enabled = true;

        t.Wait();
    }
}
