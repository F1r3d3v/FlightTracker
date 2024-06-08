namespace ProjOb.Accessors
{
    public class ObjectAccessor : BaseAccessor
    {
        public ObjectAccessor(Ref<Object?> obj)
        {
            _value = obj;

            _getValueTypeMap.Add("ID", () => obj.Value!.ID.ToString());
            _setValueMap.Add("ID", (String value) => obj.Value!.ID = UInt64.Parse(value));
        }
    }
}
