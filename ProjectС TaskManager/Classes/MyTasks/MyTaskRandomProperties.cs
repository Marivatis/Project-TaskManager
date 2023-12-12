using ProjectB_TaskManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB_TaskManager.Classes.MyTasks
{
    public class MyTaskRandomProperties
    {
        private Random random;

        public MyTaskRandomProperties() 
        {
            random = new Random();
        }

        public int GetId()
        {
            return random.Next(1, 100);
        }

        public string GetTitle()
        {
            return $"Some Title {random.Next(1, 100)}";
        }

        public string GetCourseName()
        {
            return $"Course {random.Next(1, 8)}";
        }

        public string GetDescription()
        {
            return $"Some Description {random.Next(1, 100)}";
        }

        public MyTaskStatus GetTaskStatus()
        {
            return (MyTaskStatus) random.Next(0, 4);
        }

        public DateTime GetDeadline()
        {
            int year = random.Next(2024, 2025);
            int manth = random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, manth) + 1);

            return new DateTime(year, manth, day);
        }
    }
}
