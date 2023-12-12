using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB_TaskManager.Classes.Consoles
{
    public static class Printer
    {
        public static void PrintMainMenu()
        {
            Console.WriteLine("[1] - Add task");
            Console.WriteLine("[2] - Print tasks");
            Console.WriteLine("[3] - Sort tasks by remaining date");
            Console.WriteLine("[4] - Mark task as comleted");
            Console.WriteLine("[5] - Remove task");
            Console.WriteLine("[6] - Clear task manager list");
            Console.WriteLine("[7] - Show main menu");
            Console.WriteLine("[0] - Exit");
        }

        public static void PrintAddMenu()
        {
            Console.WriteLine("[1] - Add university task");
            Console.WriteLine("[2] - Add university task randomly");
            Console.WriteLine("[3] - Add general task");
            Console.WriteLine("[4] - Add general task randomly");
        }
    }
}
