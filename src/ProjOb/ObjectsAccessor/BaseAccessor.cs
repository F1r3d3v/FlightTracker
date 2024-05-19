using System.Text;

namespace ProjOb.Accessors
{
    public abstract class BaseAccessor : IQueryAccessor
    {
        protected readonly Dictionary<String, Func<String?>> _getValueTypeMap = new(StringComparer.OrdinalIgnoreCase);
        protected readonly Dictionary<String, Action<String>> _setValueMap = new(StringComparer.OrdinalIgnoreCase);
        protected readonly Dictionary<String, IQueryAccessor> _accessorMap = new(StringComparer.OrdinalIgnoreCase);

        public virtual String? GetValue(String value)
        {
            String[] split = value.Split('.', 2);
            if (_getValueTypeMap.TryGetValue(value, out Func<String?>? fun))
            {
                return fun();
            }
            else if (_accessorMap.TryGetValue(split[0], out IQueryAccessor? accessor))
            {
                if (split.Length < 2)
                    return accessor.GetValue("*");
                else
                    return accessor.GetValue(split[1]);
            }
            else if (value == "*")
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{ ");
                sb.Append(String.Join(", ", _getValueTypeMap.Values.Select(x => x()).Concat(_accessorMap.Values.Select(x => x.GetValue("*")))));
                sb.Append(" }");

                return sb.ToString();
            }
            else
            {
                throw new ArgumentException("Invalid variable name");
            }
        }

        public virtual void SetValue(String param, String value)
        {
            String[] split = param.Split('.', 2);
            if (_setValueMap.TryGetValue(param, out Action<String>? fun))
            {
                fun(value);
            }
            else if (_accessorMap.TryGetValue(split[0], out IQueryAccessor? accessor))
            {
                accessor.SetValue(split[1], value);
            }
            else if (param == "*")
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new ArgumentException("Invalid variable name");
            }
        }

        public IEnumerable<String> GetFields()
        {
            return _getValueTypeMap.Keys.Concat(_accessorMap.Keys);
        }

        public IEnumerable<String> GetFields(String field)
        {
            return _accessorMap[field].GetFields();
        }
    }
}
