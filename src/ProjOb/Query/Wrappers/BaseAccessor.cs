using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public abstract class BaseAccessor : IQueryAccessor
    {
        protected readonly Dictionary<String, Func<String?>> _getValueMap = new(StringComparer.OrdinalIgnoreCase);
        protected readonly Dictionary<String, Action<String>> _setValueMap = new(StringComparer.OrdinalIgnoreCase);

        public virtual String? GetValue(String value)
        {
            if (value == "*")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{ ");
                sb.Append(String.Join(", ", _getValueMap.Values.Select(x => x())));
                sb.Append(" }");

                return sb.ToString();
            }
            else if (_getValueMap.TryGetValue(value, out Func<String?>? fun))
            {
                return fun();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public virtual void SetValue(String param, String value)
        {
            if (_setValueMap.TryGetValue(param, out Action<String>? fun))
            {
                fun(value);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<String> GetFields()
        {
            return _getValueMap.Keys;
        }
    }
}
