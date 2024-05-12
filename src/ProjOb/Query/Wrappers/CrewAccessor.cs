using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class CrewAccessor : PersonAccessor
    {
        public CrewAccessor(Crew crew) : base(crew)
        {
            _getValueMap.Add("Practice", crew.Practice.ToString);
            _setValueMap.Add("Practice", (String value) => crew.Practice = UInt16.Parse(value));

            _getValueMap.Add("Role", () => crew.Role);
            _setValueMap.Add("Role", (String value) => crew.Role = value);
        }
    }
}
