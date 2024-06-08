using System.Text;

namespace ProjOb.Accessors
{
    public abstract class BaseAccessor : IQueryAccessor
    {
        protected readonly Dictionary<String, Func<String?>> _getValueTypeMap = new(StringComparer.OrdinalIgnoreCase);
        protected readonly Dictionary<String, Action<String>> _setValueMap = new(StringComparer.OrdinalIgnoreCase);
        protected readonly Dictionary<String, IQueryAccessor> _accessorMap = new(StringComparer.OrdinalIgnoreCase);
        protected Ref<Object?> _value;
        protected bool _isStruct = false;

        public virtual String? GetValue(String value)
        {
            if (!_isStruct && _value.Value == null) return null;
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
            if (!_isStruct && param != "*" && _value.Value == null) return;
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
                if (UInt64.TryParse(value, out UInt64 id))
                {
                    _value.Value = db.GetObject(id);
                    if (_value.Value == null)
                        throw new ArgumentException($"No object of ID: {id} exists in a database");
                }
                else if (_isStruct)
                {
                    if (value[0] != '{' || value[^1] != '}')
                        throw new ArgumentException("Invalid struct format");

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
