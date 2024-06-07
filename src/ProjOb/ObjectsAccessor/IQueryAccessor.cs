namespace ProjOb.Accessors
{
    public interface IQueryAccessor
    {
        String? GetValue(String value);
        void SetValue(String param, String value, Database db);
        IEnumerable<String> GetFields();
        IEnumerable<String> GetFields(String field);
    }
}
