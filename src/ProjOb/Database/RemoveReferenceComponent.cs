namespace ProjOb.Components
{
    internal class RemoveReferenceComponent : IComponent
    {
        private Object _object;

        public RemoveReferenceComponent(Object obj)
        {
            _object = obj;
        }

        public void Process(Flight flight)
        {
            if (flight.Origin == _object) flight.Origin = null;
            else if (flight.Target == _object) flight.Target = null;
            else if (flight.Plane == _object) flight.Plane = null;
            flight.Crews.RemoveAll((x) => x == _object);
            flight.Loads.RemoveAll((x) => x == _object);
        }
    }
}
