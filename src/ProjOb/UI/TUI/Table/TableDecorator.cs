namespace ProjOb.UI
{
    public class TableDecorator : BaseTableDecorator
    {
        private int[] columnsMaxWidth = [];

        public TableDecorator(ITable table) : base(table) { }

        private int[] GetMaxColumnSizes()
        {
            String[] headers = Table.GetHeader();
            int[] sizes = new int[headers.Length];
            int i = 0;
            foreach (var col in Table.GetColumns())
            {
                int maxSizeHeader = headers[i].Length;
                int maxSizeData = col.MaxBy(x => x.Length)?.Length ?? 0;
                sizes[i] = Math.Max(maxSizeHeader, maxSizeData);
                i++;
            }

            return sizes;
        }

        // false - left align, true - right align
        private String FormatRow(String row, bool align = false, int padding = 1)
        {
            var cells = row.Split('|');
            var formattedCells = new List<String>();

            for (int i = 0; i < cells.Length; ++i)
            {
                String formattedCell = String.Format($"{{0, {(align ? "" : "-")}{columnsMaxWidth[i]}}}", cells[i]);
                formattedCell = formattedCell.PadLeft(formattedCell.Length + padding);
                formattedCell = formattedCell.PadRight(formattedCell.Length + padding);

                formattedCells.Add(formattedCell);
            }

            return String.Join("|", formattedCells);
        }

        public override void Display()
        {
            columnsMaxWidth = GetMaxColumnSizes();

            var originalOut = Console.Out;
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                Table.Display();
                var output = writer.ToString();
                Console.SetOut(originalOut);

                var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                var formattedLines = new List<String>
                {
                    FormatRow(lines[0]),
                    String.Join('+', columnsMaxWidth.Select(x => new String('-', x + 2))) // +2 for left & right padding
                };

                for (int j = 1; j < lines.Length; ++j)
                {
                    formattedLines.Add(FormatRow(lines[j], true));
                }

                foreach (var formattedLine in formattedLines)
                {
                    Console.WriteLine(formattedLine);
                }
            }
        }
    }
}
