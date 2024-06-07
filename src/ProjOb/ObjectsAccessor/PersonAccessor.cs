﻿namespace ProjOb.Accessors
{
    public class PersonAccessor : ObjectAccessor
    {
        public PersonAccessor(Person? person) : base(person)
        {
            if (person == null) return;

            _getValueTypeMap.Add("Name", () => person.Name);
            _setValueMap.Add("Name", (String value) => person.Name = value);

            _getValueTypeMap.Add("Age", () => person.Age.ToString());
            _setValueMap.Add("Age", (String value) => person.Age = UInt64.Parse(value));

            _getValueTypeMap.Add("Phone", () => person.Phone);
            _setValueMap.Add("Phone", (String value) => person.Phone = value);

            _getValueTypeMap.Add("Email", () => person.Email);
            _setValueMap.Add("Email", (String value) => person.Email = value);
        }
    }
}
