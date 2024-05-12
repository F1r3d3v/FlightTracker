using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.Wrappers
{
    public class PersonAccessor : ObjectAccessor
    {
        public PersonAccessor(Person person) : base(person)
        {
            _getValueMap.Add("Name", () => person.Name);
            _setValueMap.Add("Name", (String value) => person.Name = value);

            _getValueMap.Add("Age", person.Age.ToString);
            _setValueMap.Add("Age", (String value) => person.Age = UInt64.Parse(value));
            
            _getValueMap.Add("Phone", () => person.Phone);
            _setValueMap.Add("Phone", (String value) => person.Phone = value);
            
            _getValueMap.Add("Email", () => person.Email);
            _setValueMap.Add("Email", (String value) => person.Email = value);
        }
    }
}
