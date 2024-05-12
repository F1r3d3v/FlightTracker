using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    internal class AccessorVisitor : IComponent<IQueryAccessor>
    {
        public IQueryAccessor Process(Crew crew)
        {
            return new CrewAccessor(crew);
        }

        public IQueryAccessor Process(Passenger passengger)
        {
            return new PassengerAccessor(passengger);
        }

        public IQueryAccessor Process(Cargo cargo)
        {
            return new CargoAccessor(cargo);
        }

        public IQueryAccessor Process(CargoPlane cargoPlane)
        {
            return new CargoPlaneAccessor(cargoPlane);
        }

        public IQueryAccessor Process(PassengerPlane passengerPlane)
        {
            return new PassengerPlaneAccessor(passengerPlane);
        }

        public IQueryAccessor Process(Airport airport)
        {
            return new AirportAccessor(airport);
        }

        public IQueryAccessor Process(Flight flight)
        {
            return new FlightAccessor(flight);
        }
    }
}
