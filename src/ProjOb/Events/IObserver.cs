using NetworkSourceSimulator;

namespace ProjOb.Events
{
    public interface IObserver
    {
        void OnDataChanged(object sender, IDUpdateArgs args) { }
        void OnDataChanged(object sender, PositionUpdateArgs args) { }
        void OnDataChanged(object sender, ContactInfoUpdateArgs args) { }
    }
}
