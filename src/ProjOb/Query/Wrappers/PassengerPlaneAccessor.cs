using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class PassengerPlaneAccessor : PlaneAccessor
    {
        public PassengerPlaneAccessor(PassengerPlane passengerPlane) : base(passengerPlane)
        {
            _getValueMap.Add("FirstClassSize", passengerPlane.FirstClassSize.ToString);
            _setValueMap.Add("FirstClassSize", (String value) => passengerPlane.FirstClassSize = UInt16.Parse(value));

            _getValueMap.Add("BusinessClassSize", passengerPlane.BusinessClassSize.ToString);
            _setValueMap.Add("BusinessClassSize", (String value) => passengerPlane.BusinessClassSize = UInt16.Parse(value));

            _getValueMap.Add("EconomyClassSize", passengerPlane.EconomyClassSize.ToString);
            _setValueMap.Add("EconomyClassSize", (String value) => passengerPlane.EconomyClassSize = UInt16.Parse(value));
        }
    }
}
