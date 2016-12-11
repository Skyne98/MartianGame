using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianGameLevelEditor
{
    public class ConsoleChar
    {
        public ConsoleColor ForeColor = ConsoleColor.Gray;
        public ConsoleColor BackColor = ConsoleColor.Black;
        public char Char = '#';
        public int X, Y;

        public ConsoleChar(int x, int y)
        {
            X = x;
            Y = y;
        }
        public ConsoleChar(int x, int y, ConsoleColor foreColor, ConsoleColor backColor, char ch)
        {
            X = x;
            Y = y;

            ForeColor = foreColor;
            BackColor = backColor;
            Char = ch;
        }

        public void Reset()
        {
            ForeColor = ConsoleColor.Gray;
            BackColor = ConsoleColor.Black;
            Char = '#';
        }

        public void Draw()
        {
            Console.SetCursorPosition(X, ConsoleWrapper.GetInstance().Height - Y);
            Console.ForegroundColor = ForeColor;
            Console.BackgroundColor = BackColor;
            Console.Write(Char);
        }
    }
}
