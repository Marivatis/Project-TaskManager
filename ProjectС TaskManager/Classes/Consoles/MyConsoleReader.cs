using ProjectС_TaskManager.Classes.MyTasks;
using ProjectС_TaskManager.Enums;
using System;
using System.Globalization;

namespace ProjectС_TaskManager.Classes.Consoles
{
    internal class MyConsoleReader : Reader
    {
        public static int ReadInt32(string inputMessage, int minRange, int maxRange)
        {
            int number;

            try
            {
                Console.Write(inputMessage);
                number = ReadInt32(minRange, maxRange);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Please, select only available options!");
                number = ReadInt32(inputMessage, minRange, maxRange);
            }
            catch (Exception)
            {
                Console.WriteLine("Please, input only integer number!");
                number = ReadInt32(inputMessage, minRange, maxRange);
            }

            return number;
        }
        public static int ReadInt32(string inputMessage)
        {
            int number;

            try
            {
                Console.Write(inputMessage);
                number = ReadInt32();
            }
            catch (OverflowException)
            {
                Console.WriteLine("Please, select only available options!");
                number = ReadInt32(inputMessage);
            }
            catch (Exception)
            {
                Console.WriteLine("Please, input only integer number!");
                number = ReadInt32(inputMessage);
            }

            return number;
        }

        public static string ReadString(string inputMessage)
        {
            Console.Write(inputMessage);
            return Console.ReadLine();
        }

        public static DateTime ReadDateTime(string inputMessage, string format)
        {
            DateTime date = DateTime.MinValue;

            try
            {
                Console.Write(inputMessage);

                date = ReadDateTime(format);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ReadDateTime(inputMessage, format);
            }

            return date;
        }

        public static Type ReadMyTaskType()
        {
            string typeString = ReadString("Enter task type [University/General] --> ");

            Type type = typeString.Equals("General") ? typeof(MyGeneralTask) : typeof(MyUniversityTask);

            return type;
        }

        public static T ReadEnum<T>(string inputMessage)
            where T : Enum
        {
            T enumValue = default;

            try
            {
                Console.Write(inputMessage);

                enumValue = ReadEnum<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ReadEnum<T>(inputMessage);
            }

            return enumValue;
        }
    }
}
