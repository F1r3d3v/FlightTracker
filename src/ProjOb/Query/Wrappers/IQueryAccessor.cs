using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public interface IQueryAccessor
    {
        String? GetValue(String value);
        void SetValue(String param, String value);
    }
}
