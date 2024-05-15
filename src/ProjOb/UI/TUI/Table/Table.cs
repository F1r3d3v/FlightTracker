namespace ProjOb.UI
{
    public class Table : ITable
    {
        public int ColumnsCount { get => _headerNames.Count; }
        public int RowsCount { get =>  _rowsData.Count; }

        private readonly List<List<String>> _rowsData = [];
        private readonly List<String> _headerNames = [];

        public void AddColumn(String name)
        {
            if (_headerNames.Contains(name)) return;
            _headerNames.Add(name);

            String[] col = new String[RowsCount];
            Array.Fill(col, "Null");

            for (int i = 0; i < RowsCount; i++)
            {
                _rowsData[i].Add(col[i]);
            }
        }

        public void AddRow(List<String> record)
        {
            if (record.Count > ColumnsCount)
            {
                record = record.Take(ColumnsCount).ToList();
            }
            else if (record.Count < ColumnsCount)
            {
                record.AddRange(Enumerable.Repeat("Null", ColumnsCount - record.Count));
            }
            _rowsData.Add(record);
        }

        public void RemoveRow(int ind)
        {
            if (ind >= RowsCount || ind < 0) throw new IndexOutOfRangeException();
            _rowsData.RemoveAt(ind);
        }

        public void RemoveColumn(String name)
        {
            if (!_headerNames.Contains(name)) return;
            _headerNames.Remove(name);
            int ind = _rowsData[0].FindIndex(x => x == name);
            for (int i = 0; i < RowsCount; ++i)
            {
                _rowsData[i].RemoveAt(ind);
            }
        }

        public virtual void Display()
        {
            Console.WriteLine(String.Join("|", _headerNames));
            foreach (var row in _rowsData)
            {
                Console.WriteLine(String.Join("|", row));
            }
        }

        public IEnumerator<List<String>> GetRows()
        {
            return _rowsData.GetEnumerator();
        }

        public String[] GetHeader()
        {
            return _headerNames.ToArray();
        }

        public IEnumerable<List<String>> GetColumns()
        {
            for (int i = 0; i < ColumnsCount; ++i)
            {
                List<String> col = new List<String>();
                for (int j = 0; j < RowsCount; ++j)
                {
                    col.Add(_rowsData[j][i]);
                }
                yield return col;
            }
        }
    }
}
