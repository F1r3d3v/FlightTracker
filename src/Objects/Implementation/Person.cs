namespace ProjOb
{
    public abstract class Person : Object
    {
        public String? Name { get; set; }
        public UInt64 Age { get; set; }
        public String? Phone { get; set; }
        public String? Email { get; set; }
    }
}