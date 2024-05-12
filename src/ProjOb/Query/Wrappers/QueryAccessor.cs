using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class QueryAccessor : IQueryAccessor
    {
        private readonly Dictionary<String, Func<String>> _getValueMap;
        private readonly Dictionary<String, Action<String>> _setValueMap;

        public QueryAccessor(Dictionary<String, Func<String>> getter, Dictionary<String, Action<String>> setter)
        {
            _getValueMap = getter;
            _setValueMap = setter;
        }

        public String GetValue(String obj)
        {
            return _getValueMap[obj]();
        }

        public void SetValue(String obj, String value)
        {
            _setValueMap[obj](value);
        }
    }
}
