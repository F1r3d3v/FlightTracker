using BruTile.Wms;
using DynamicData;
using Mapsui.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class FlightAccessor : ObjectAccessor
    {
        private readonly Flight _flight;

        public FlightAccessor(Flight flight) : base(flight)
        {
            _flight = flight;

            _getValueMap.Add("TakeoffTime", flight.TakeoffTime.ToString);
            _setValueMap.Add("TakeoffTime", (String value) => flight.TakeoffTime = TimeSpan.Parse(value));

            _getValueMap.Add("LandingTime", flight.LandingTime.ToString);
            _setValueMap.Add("LandingTime", (String value) => flight.LandingTime = TimeSpan.Parse(value));

            _getValueMap.Add("AMSL", flight.AMSL.ToString);
            _setValueMap.Add("AMSL", (String value) => flight.AMSL = Single.Parse(value));
        }

        public override String? GetValue(String value)
        {
            var split = value.Split('.', 2).ToList();
            if (split.Count < 2) split.Add("*");

            if (split[0] == "Origin")
            {
                return new AirportAccessor(_flight.Origin!).GetValue(split[1]);
            }
            else if (split[0] == "Target")
            {
                return new AirportAccessor(_flight.Target!).GetValue(split[1]);
            }
            else if(split[0] == "WorldPosition")
            {
                var pos = new WorldPosition(_flight.Latitude, _flight.Longitude);
                return new WorldPositionAccessor(pos).GetValue(split[1]);
            }
            else if (split[0] == "Plane")
            {
                return new PlaneAccessor(_flight.Plane!).GetValue(split[1]);
            }
            else
            {
                return base.GetValue(value);
            }
        }

        public override void SetValue(string param, string value)
        {
            String[] split = value.Split('.', 2);
            if (split[0] == "Origin")
            {
                var accessor = new AirportAccessor(_flight.Origin!);
                accessor.SetValue(split[1], value);
            }
            else if (split[0] == "Target")
            {
                var accessor = new AirportAccessor(_flight.Target!); ;
                accessor.SetValue(split[1], value);
            }
            else if(split[0] == "WorldPosition")
            {
                var pos = new WorldPosition(_flight.Latitude, _flight.Longitude);
                var accessor = new WorldPositionAccessor(pos);
                accessor.SetValue(split[1], value);
                _flight.Longitude = (float)accessor.Position.Longitude;
                _flight.Latitude = (float)accessor.Position.Latitude;
            }
            else if (split[0] == "Plane")
            {
                var accessor = new PlaneAccessor(_flight.Plane!);
                accessor.SetValue(split[1], value);
            }
            else
            {
                base.SetValue(param, value);
            }
        }
    }
}
