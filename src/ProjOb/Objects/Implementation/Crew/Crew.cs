namespace ProjOb
{
    public class Crew : Person
    {
        public UInt16 Practice { get; set; }
        public String? Role { get; set; }

        internal Crew(CrewDTO data)
        {
            Type = data.Type;
            ID = data.ID;
            Name = data.Name;
            Age = data.Age;
            Phone = data.Phone;
            Email = data.Email;
            Practice = data.Practice;
            Role = data.Role;
        }
    }
}