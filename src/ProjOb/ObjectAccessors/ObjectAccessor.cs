using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class ObjectAccessor : BaseAccessor
    {
        public ObjectAccessor(Object obj)
        {
            _getValueTypeMap.Add("ID", () => obj.ID.ToString());
            _setValueMap.Add("ID", (String value) => obj.ID = UInt64.Parse(value));
        }
    }
}
