using ProjectС_TaskManager.Enums;
using System;
using System.Globalization;

namespace ProjectС_TaskManager.Classes.Consoles
{
    public class Reader
    {
        public static int ReadInt32()
        {
            return Convert.ToInt32(Console.ReadLine());
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
            string input = Console.ReadLine();
            DateTime date = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);

            return date;
        }

        public static T ReadEnum<T>()
            where T : Enum
        {
            string input = Console.ReadLine();

            return (T) Enum.Parse(typeof(T), input);
        }
    }
}
