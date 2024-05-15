using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class AirportAccessor : ObjectAccessor
    {
        private readonly Airport _airport;

        public AirportAccessor(Airport airport) : base(airport)
        {
            _airport = airport;

            _getValueMap.Add("Name", () => airport.Name);
            _setValueMap.Add("Name", (String value) => airport.Name = value);

            _getValueMap.Add("Code", () => airport.Code);
            _setValueMap.Add("Code", (String value) => airport.Code = value);

            _getValueMap.Add("AMSL", airport.AMSL.ToString);
            _setValueMap.Add("AMSL", (String value) => airport.AMSL = Single.Parse(value));

            _getValueMap.Add("Country", () => airport.Country);
            _setValueMap.Add("Country", (String value) => airport.Country = value);
        }

        public override String? GetValue(String value)
        {
            var split = value.Split('.', 2).ToList();
            if (split.Count < 2) split.Add("*");

            if (split[0] == "WorldPosition")
            {
                var pos = new WorldPosition(_airport.Latitude, _airport.Longitude);
                return new WorldPositionAccessor(pos).GetValue(split[1]);
            }
            else
            {
                return base.GetValue(value);
            }
        }

        public override void SetValue(string param, string value)
        {
            String[] split = value.Split('.', 2);
            if (split[0] == "WorldPosition")
            {
                var pos = new WorldPosition(_airport.Latitude, _airport.Longitude);
                var accessor = new WorldPositionAccessor(pos);
                accessor.SetValue(split[1], value);
                _airport.Longitude = (float)accessor.Position.Longitude;
                _airport.Latitude = (float)accessor.Position.Latitude;
            }
            else
            {
                base.SetValue(param, value);
            }
        }
    }
}
