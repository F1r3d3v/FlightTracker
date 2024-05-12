using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public abstract class BaseAccessor : IQueryAccessor
    {
        protected readonly Dictionary<String, Func<String?>> _getValueMap = [];
        protected readonly Dictionary<String, Action<String>> _setValueMap = [];

        public virtual String? GetValue(String value)
        {
            if (_getValueMap.TryGetValue(value, out Func<String?>? fun))
            {
                return fun();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public virtual void SetValue(string param, string value)
        {
            if (_setValueMap.TryGetValue(param, out Action<String>? fun))
            {
                fun!(value);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
