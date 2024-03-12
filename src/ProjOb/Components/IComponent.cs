namespace ProjOb
{
    public interface IComponent
    {
        void Process(Crew crew);
        void Process(Passenger passengger);
        void Process(Cargo cargo);
        void Process(CargoPlane cargoPlane);
        void Process(PassengerPlane passengerPlane);
        void Process(Airport airport);
        void Process(Flight flight);
    }
}
