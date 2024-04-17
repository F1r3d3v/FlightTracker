using NetworkSourceSimulator;

namespace ProjOb.Events
{
    public interface IObjectObserver
    {
        void OnIDChanged(object sender, IDUpdateArgs args);
        void OnPositionChanged(object sender, PositionUpdateArgs args);
        void OnContactInfoChanged(object sender, ContactInfoUpdateArgs args);
    }
}
