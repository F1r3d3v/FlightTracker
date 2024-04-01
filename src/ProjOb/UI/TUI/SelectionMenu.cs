namespace ProjOb.UI
{
    internal class SelectionMenu
    {
        private readonly string[] _entries = [];
        private string? _title = null;
        private int _selection = 0;
        private int _selectionsCount = 0;

        internal SelectionMenu(string[] entries, string? title)
        {
            _entries = entries;
            _selectionsCount = entries.Length;
            _title = title;
        }

        internal int Show()
        {
            bool selected = false;
            TerminalHelper.SetCursorVisibility(false);
            TerminalHelper.ClearScreen(TerminalHelper.ClearScreenType.EntireScreenWithBuffer);

            do
            {
                TerminalHelper.MoveCursorToHome();
                TerminalHelper.ClearScreen(TerminalHelper.ClearScreenType.FromCurToEnd);

                if (_title != null)
                {
                    TerminalHelper.MoveCursorAbsolute(4, 5);
                    Console.WriteLine(_title);
                }

                for (int i = 0; i < _entries.Length; i++)
                    CreateEntry(_entries[i], (uint)(5 + i), 5, _selection == i);

                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (--_selection < 0) _selection = 0;
                        break;

                    case ConsoleKey.DownArrow:
                        if (++_selection >= _selectionsCount) _selection = _selectionsCount - 1;
                        break;

                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                }
            }
            while (!selected);

            TerminalHelper.MoveCursorToHome();
            TerminalHelper.ClearScreen(TerminalHelper.ClearScreenType.FromCurToEnd);
            TerminalHelper.ClearScreen(TerminalHelper.ClearScreenType.EntireScreenWithBuffer);

            return _selection;
        }

        private void CreateEntry(string entry, uint row, uint column, bool hovered)
        {
            TerminalHelper.MoveCursorAbsolute(row, column);

            if (hovered)
                entry = TerminalHelper.SetTextBasicColorLegacy(entry, TerminalHelper.TermColor.Black, TerminalHelper.TermColor.White);

            Console.WriteLine($"{(hovered ? ">" : " ")} {entry}");
        }

        public static int CreateSelectionMenu(string[] entries, string? title = null)
        {
            SelectionMenu menu = new SelectionMenu(entries, title);
            return menu.Show();
        }

        public static void CreateSelectionMenu(Dictionary<string, Action> entries, string? title = null)
        {
            string[] ent = entries.Keys.ToArray();
            SelectionMenu menu = new SelectionMenu(entries.Keys.ToArray(), title);
            int selection = menu.Show();
            entries.GetValueOrDefault(ent[selection])?.Invoke();
        }
    }
}
