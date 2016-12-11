using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianGameLevelEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleWrapper consoleWrapper = ConsoleWrapper.GetInstance();

            consoleWrapper.StartCycle();
        }
    }
}
