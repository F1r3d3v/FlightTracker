using System.Text;

namespace ProjOb.Accessors
{
    public abstract class BaseAccessor : IQueryAccessor
    {
        protected readonly Dictionary<String, Func<String?>> _getValueTypeMap = new(StringComparer.OrdinalIgnoreCase);
        protected readonly Dictionary<String, Action<String>> _setValueMap = new(StringComparer.OrdinalIgnoreCase);
        protected readonly Dictionary<String, IQueryAccessor> _accessorMap = new(StringComparer.OrdinalIgnoreCase);
        protected Object? _value;
        protected bool _isStruct = false;

        public virtual String? GetValue(String value)
        {
            if (_value == null && !_isStruct) return null;
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

        public virtual void SetValue(String param, String value, Database db)
        {
            String[] split = param.Split('.', 2);
            if (_setValueMap.TryGetValue(param, out Action<String>? fun))
            {
                fun(value);
            }
            else if (_accessorMap.TryGetValue(split[0], out IQueryAccessor? accessor))
            {
                if (split.Length == 2)
                    accessor.SetValue(split[1], value, db);
                else
                    accessor.SetValue("*", value, db);
            }
            else if (param == "*")
            {
                _value = db.GetObject(UInt64.Parse(value));
                if (_value == null)
                {
                    int counter = 0;
                    String[] values = value[1..^1].Split(',');
                    var fields = GetFields();

                    if (values.Length != fields.Count())
                        throw new ArgumentException("Invalid values count");

                    foreach (String field in fields)
                    {
                        SetValue(field, values[counter++], db);
                    }
                }
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
