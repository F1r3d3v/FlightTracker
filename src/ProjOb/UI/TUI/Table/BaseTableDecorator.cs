namespace ProjOb.UI
{
    public abstract class BaseTableDecorator : ITable
    {
        protected ITable Table;

        public BaseTableDecorator(ITable table)
        {
            Table = table;
        }

        public void AddColumn(string name) => Table.AddColumn(name);

        public void AddRow(List<string> record) => Table.AddRow(record);

        public void RemoveColumn(string name) => Table.RemoveColumn(name);

        public void RemoveRow(int ind) => Table.RemoveRow(ind);

        public IEnumerable<List<string>> GetColumns() => Table.GetColumns();

        public IEnumerator<List<string>> GetRows() => Table.GetRows();

        public String[] GetHeader() => Table.GetHeader();

        public abstract void Display();
    }
}