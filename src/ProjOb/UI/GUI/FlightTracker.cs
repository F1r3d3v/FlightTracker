using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
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

            while (!(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime) || lifetime.MainWindow is null || !lifetime.MainWindow.IsLoaded)
                Thread.Sleep(1);

            FlightsGUIData data = wrapper.ConvertToFlightsGUIData(DateTime.Now);
            Runner.UpdateGUI(data);
            Console.WriteLine($"Current Time: {DateTime.Now:HH:mm:ss}");
            timer.Enabled = true;

            var window = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.MainWindow!;
            while (window.IsLoaded)
                Thread.Sleep(1);

            timer.Dispose();
        }
    }
}
