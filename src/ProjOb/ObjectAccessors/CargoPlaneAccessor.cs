using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class CargoPlaneAccessor : PlaneAccessor
    {
        public CargoPlaneAccessor(CargoPlane cargoPlane) : base(cargoPlane)
        {
            _getValueTypeMap.Add("MaxLoad", () =>  cargoPlane.MaxLoad.ToString());
            _setValueMap.Add("MaxLoad", (String value) => cargoPlane.MaxLoad = Single.Parse(value));
        }
    }
}
