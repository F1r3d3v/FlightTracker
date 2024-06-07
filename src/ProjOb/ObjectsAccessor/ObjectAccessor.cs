namespace ProjOb.Accessors
{
    public class ObjectAccessor : BaseAccessor
    {
        public ObjectAccessor(Object? obj)
        {
            _value = obj;
            if (obj == null) return;

            _getValueTypeMap.Add("ID", () => obj.ID.ToString());
            _setValueMap.Add("ID", (String value) => obj.ID = UInt64.Parse(value));
        }
    }
}
