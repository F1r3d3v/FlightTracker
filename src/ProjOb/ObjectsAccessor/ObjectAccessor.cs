namespace ProjOb.Accessors
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
