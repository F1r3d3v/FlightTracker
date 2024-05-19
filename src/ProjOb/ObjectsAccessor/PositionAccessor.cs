namespace ProjOb.Accessors
{
    public sealed class Ref<T>
    {
        private readonly Func<T> getter;
        private readonly Action<T> setter;
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
        public T Value { get { return getter(); } set { setter(value); } }
    }

    public class PositionAccessor : BaseAccessor
    {
        public PositionAccessor(Ref<Single> longitude, Ref<Single> latitude)
        {
            _getValueTypeMap.Add("Long", () => longitude.Value.ToString());
            _setValueMap.Add("Long", (String value) => longitude.Value = Single.Parse(value));

            _getValueTypeMap.Add("Lat", () => latitude.Value.ToString());
            _setValueMap.Add("Lat", (String value) => latitude.Value = Single.Parse(value));
        }
    }
}
