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
            { if ((Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime) && lifetime.MainWindow!.IsLoaded) return; }

            Task t = Task.Run(() => Runner.Run());

            IFlightsGUIDataDecorator positionDecorator = new DefaultFlightsGUIDataDecorator();
            positionDecorator = new CustomFlightsGUIDataDecorator(positionDecorator);
            IFlightTrackerAdapter wrapper = new FlightsGUIDataAdapter(positionDecorator);

            Timer timer = new(1000);
            timer.Elapsed += (object? sender, ElapsedEventArgs e) =>
            {
                FlightsGUIData data = wrapper.ConvertToFlightsGUIData(e.SignalTime, db);
                Runner.UpdateGUI(data);
            };
            timer.AutoReset = true;

            while (!(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime) || lifetime.MainWindow is null || !lifetime.MainWindow.IsLoaded)
                Thread.Sleep(1);

            FlightsGUIData data = wrapper.ConvertToFlightsGUIData(DateTime.Now, db);
            Runner.UpdateGUI(data);
            timer.Enabled = true;

            var window = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)!.MainWindow!;
            while (window.IsLoaded)
                Thread.Sleep(1);

            timer.Dispose();
        }
    }
}
