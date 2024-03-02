namespace ProjOb
{
    public class Passenger : Person
    {
        public String? Class { get; set; }
        public UInt64 Miles { get; set; }

        internal Passenger(PassengerDTO data)
        {
            Type = data.Type;
            ID = data.ID;
            Name = data.Name;
            Age = data.Age;
            Phone = data.Phone;
            Email = data.Email;
            Class = data.Class;
            Miles = data.Miles;
        }
    }
}