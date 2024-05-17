using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class PassengerAccessor : PersonAccessor
    {
        public PassengerAccessor(Passenger passenger) : base(passenger)
        {
            _getValueTypeMap.Add("Class", () => passenger.Class);
            _setValueMap.Add("Class", (String value) => passenger.Class = value);

            _getValueTypeMap.Add("Miles", () => passenger.Miles.ToString());
            _setValueMap.Add("Miles", (String value) => passenger.Miles = UInt64.Parse(value));
        }
    }
}
