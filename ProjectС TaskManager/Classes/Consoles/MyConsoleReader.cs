using ProjectB_TaskManager.Enums;
using System;
using System.Globalization;

namespace ProjectB_TaskManager.Classes.Consoles
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

        public static MyTaskStatus ReadMyTaskStatus(string inputMessage)
        {
            MyTaskStatus status = MyTaskStatus.NotStarted;

            try
            {
                Console.Write(inputMessage);

                status = ReadMyTaskStatus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return status;
        }
    }
}
