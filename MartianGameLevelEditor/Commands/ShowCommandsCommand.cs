using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianGameLevelEditor.Commands
{
    public class ShowCommandsCommand : ICommand
    {
        public void Call()
        {
            ConsoleWrapper.GetInstance().ShowCommands = !ConsoleWrapper.GetInstance().ShowCommands;

            if (ConsoleWrapper.GetInstance().ShowCommands == false)
            {
                ConsoleWrapper.GetInstance().InterfaceLayer = 
            }
        }
    }
}
