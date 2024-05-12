using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class WorldPositionAccessor : BaseAccessor
    {
        public WorldPosition Position;

        public WorldPositionAccessor(WorldPosition pos)
        {
            Position = pos;
            _getValueMap.Add("Lon", () => Position.Longitude.ToString());
            _setValueMap.Add("Lon", (String value) => Position.Longitude = double.Parse(value));

            _getValueMap.Add("Lat", () => Position.Latitude.ToString());
            _setValueMap.Add("Lat", (String value) => Position.Latitude = double.Parse(value));
        }
    }
}
