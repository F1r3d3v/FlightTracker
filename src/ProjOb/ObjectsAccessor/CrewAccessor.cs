namespace ProjOb.Accessors
{
    public class CrewAccessor : PersonAccessor
    {
        public CrewAccessor(Crew? crew) : base(crew)
        {
            if (crew == null) return;

            _getValueTypeMap.Add("Practice", () => crew.Practice.ToString());
            _setValueMap.Add("Practice", (String value) => crew.Practice = UInt16.Parse(value));

            _getValueTypeMap.Add("Role", () => crew.Role);
            _setValueMap.Add("Role", (String value) => crew.Role = value);
        }
    }
}
