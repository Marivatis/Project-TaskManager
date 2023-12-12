using ProjectС_TaskManager.Enums;
using System;
using System.Globalization;

namespace ProjectС_TaskManager.Classes.Consoles
{
    public class Reader
    {
        protected delegate string ConsoleReadLine();

        protected static ConsoleReadLine ReadLine => ReadNonEmptyLine;

        protected static string ReadNonEmptyLine()
        {
            string line = Console.ReadLine();

            if (string.IsNullOrEmpty(line))
            {
                throw new ArgumentException(line, "Your input can`t be null or empty!");
            }

            return line;
        }

        public static int ReadInt32()
        {
            return Convert.ToInt32(ReadLine.Invoke());
        }

        public static int ReadInt32(int minRange, int maxRange)
        {
            int number = ReadInt32();

            if (minRange > number || number > maxRange)
                throw new OverflowException("Please, input number from the specified range!");

            return number;
        }

        public static DateTime ReadDateTime(string format)
        {
            string input = ReadLine.Invoke();

            DateTime date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);

            return date;
        }

        public static T ReadEnum<T>()
            where T : Enum
        {
            string input = ReadLine.Invoke();

            return (T) Enum.Parse(typeof(T), input);
        }
    }
}
