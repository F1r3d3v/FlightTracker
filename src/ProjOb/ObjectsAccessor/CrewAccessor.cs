namespace ProjOb.Accessors
{
    public class CrewAccessor : PersonAccessor
    {
        public CrewAccessor(Ref<Crew?> crew) : base(new Ref<Person?>(() => crew.Value, (x) => crew.Value = (Crew?)x))
        {
            _getValueTypeMap.Add("Practice", () => crew.Value!.Practice.ToString());
            _setValueMap.Add("Practice", (String value) => crew.Value!.Practice = UInt16.Parse(value));

            _getValueTypeMap.Add("Role", () => crew.Value!.Role);
            _setValueMap.Add("Role", (String value) => crew.Value!.Role = value);
        }
    }
}
