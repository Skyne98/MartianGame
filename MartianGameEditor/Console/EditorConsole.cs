using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunshineConsole;
using OpenTK.Graphics;
using OpenTK.Input;

namespace MartianGameEditor.Console
{
    public class EditorConsole
    {
        static EditorConsole _instance;

        public ConsoleWindow Window;

        public static EditorConsole GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EditorConsole();
            }

            return _instance;
        }

        private EditorConsole()
        {
            Window = new ConsoleWindow();
        }
    }
}
