namespace ProjOb.UI
{
    public interface ITable
    {
        void AddColumn(String name);
        void AddRow(List<String> record);
        void RemoveColumn(String name);
        void RemoveRow(int ind);
        IEnumerable<List<String>> GetColumns();
        IEnumerator<List<String>> GetRows();
        String[] GetHeader();
        void Display();
    }
}
