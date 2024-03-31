
namespace ProjOb
{
    public interface IComponent<TResult>
    {
        TResult? Process(Crew crew) => default;
        TResult? Process(Passenger passengger) => default;
        TResult? Process(Cargo cargo) => default;
        TResult? Process(CargoPlane cargoPlane) => default;
        TResult? Process(PassengerPlane passengerPlane) => default;
        TResult? Process(Airport airport) => default;
        TResult? Process(Flight flight) => default;
    }

    public interface IComponent
    {
        void Process(Crew crew) { }
        void Process(Passenger passengger) { }
        void Process(Cargo cargo) { }
        void Process(CargoPlane cargoPlane) { }
        void Process(PassengerPlane passengerPlane) { }
        void Process(Airport airport) { }
        void Process(Flight flight) { }
    }
}
