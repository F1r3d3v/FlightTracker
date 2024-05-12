using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class PlaneAccessor : ObjectAccessor
    {
        public PlaneAccessor(Plane plane) : base(plane)
        {
            _getValueMap.Add("Serial", () => plane.Serial);
            _setValueMap.Add("Serial", (String value) => plane.Serial = value);

            _getValueMap.Add("Country", () => plane.Country);
            _setValueMap.Add("Country", (String value) => plane.Country = value);

            _getValueMap.Add("Model", () => plane.Model);
            _setValueMap.Add("Model", (String value) => plane.Model = value);
        }
    }
}
