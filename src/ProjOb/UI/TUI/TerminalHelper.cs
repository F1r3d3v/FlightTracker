using System.Drawing;

namespace ProjOb.UI
{
    internal static class TerminalHelper
    {
        public enum TermColor
        {
            Black = 30,
            Red,
            Green,
            Yellow,
            Blue,
            Magenta,
            Cyan,
            White,
            BrightBlack = 90,
            BrightRed,
            BrightGreen,
            BrightYellow,
            BrightBlue,
            BrightMagenta,
            BrightCyan,
            BrightWhite
        }

        public enum CursorDir
        {
            Up = 65,
            Down,
            Forward,
            Backward
        }

        public enum ClearScreenType
        {
            FromCurToEnd,
            FromStartToCur,
            EntireScreen,
            EntireScreenWithBuffer
        }

        public enum ClearLineType
        {
            FromCurToEnd,
            FromStartToCur,
            EntireLine,
        }

        public static string SetTextBasicColorLegacy(string text, TermColor fgColor, TermColor bgColor, bool bold = false)
        {
            return $"\x1b[{(bold ? "1;" : "")}{(int)fgColor};{(int)bgColor + 10}m{text}\x1b[0m";
        }

        public static string SetTextBold(string text)
        {
            return $"\x1b[1m{text}\x1b[0m";
        }

        public static string SetTextBasicColor(string text, TermColor fgColor, TermColor bgColor)
        {
            return $"\x1b[38;5;{(int)fgColor - 30}m\x1b[48;5;{(int)bgColor - 82}m{text}\x1b[0m";
        }

        public static string SetTextForegroundColor(string text, uint r, uint g, uint b) // 0 <= r,g,b <= 5
        {
            uint color = 16 + 36 * r + 6 * g + b;
            return $"\x1b[38;5;{color}m{text}\x1b[0m";
        }

        public static string SetTextBackgroundColor(string text, uint r, uint g, uint b) // 0 <= r,g,b <= 5
        {
            uint color = 16 + 36 * r + 6 * g + b;
            return $"\x1b[48;5;{color}m{text}\x1b[0m";
        }

        public static string SetTextForegroundGrayscaleColor(string text, uint scale) // 0 <= scale <= 23
        {
            return $"\x1b[38;5;{scale + 232}m{text}\x1b[0m";
        }

        public static string SetTextBackgroundGrayscaleColor(string text, uint scale) // 0 <= scale <= 23
        {
            return $"\x1b[48;5;{scale + 232}m{text}\x1b[0m";
        }

        public static string SetTextForegroundTrueColor(string text, Color color)
        {
            return $"\x1b[38;2;{color.R};{color.G};{color.B}m{text}\x1b[0m";
        }

        public static string SetTextBackgroundTrueColor(string text, Color color)
        {
            return $"\x1b[48;2;{color.R};{color.G};{color.B}m{text}\x1b[0m";
        }

        public static void MoveCursorRelative(CursorDir dir, uint units)
        {
            Console.Write($"\x1b[{units}{(char)dir}");
        }

        public static void MoveCursorAbsolute(uint row, uint column)
        {
            Console.Write($"\x1b[{row};{column}H");
        }

        public static void MoveCursorToHome()
        {
            Console.Write($"\x1b[H");
        }

        public static void MoveCursorToColumn(uint column)
        {
            Console.Write($"\x1b[{column}G");
        }

        public static void MoveCursorToNextLine(CursorDir dir, uint units)
        {
            if (dir == CursorDir.Up)
                Console.Write($"\x1b[{units}E");
            else if (dir == CursorDir.Down)
                Console.Write($"\x1b[{units}F");
        }

        public static void SetCursorVisibility(bool show)
        {
            Console.Write($"\x1b[?25{(show ? "h" : "l")}");
        }

        public static void ClearScreen(ClearScreenType type)
        {
            Console.Write($"\x1b[{(int)type}J");
        }

        public static void ClearLine(ClearLineType type)
        {
            Console.Write($"\x1b[{(int)type}K");
        }
    }
}
