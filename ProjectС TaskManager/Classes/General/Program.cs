using ProjectB_TaskManager.Classes.Consoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB_TaskManager.Classes.General
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyConsole myConsole = new MyConsole();
            myConsole.Run();
        }
    }
}
