namespace ProjOb.Accessors
{
    public class PersonAccessor : ObjectAccessor
    {
        public PersonAccessor(Ref<Person?> person) : base((Ref<Object?>)(object)person)
        {
            _getValueTypeMap.Add("Name", () => person.Value!.Name);
            _setValueMap.Add("Name", (String value) => person.Value!.Name = value);

            _getValueTypeMap.Add("Age", () => person.Value!.Age.ToString());
            _setValueMap.Add("Age", (String value) => person.Value!.Age = UInt64.Parse(value));

            _getValueTypeMap.Add("Phone", () => person.Value!.Phone);
            _setValueMap.Add("Phone", (String value) => person.Value!.Phone = value);

            _getValueTypeMap.Add("Email", () => person.Value!.Email);
            _setValueMap.Add("Email", (String value) => person.Value!.Email = value);
        }
    }
}
