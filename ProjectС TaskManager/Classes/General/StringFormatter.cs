using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB_TaskManager.Classes.General
{
    public class StringFormatter
    {
        public string FormatToLength(string str, int length)
        {
            if (str.Length > length)
            {
                return FormatToSmallerLength(str, length);
            }
            else if (str.Length < length)
            {
                return FormatToBiggerLength(str, length);
            }
            else
            {
                return str;
            }
        }

        private string FormatToSmallerLength(string str, int length)
        {
            StringBuilder formatedString = new StringBuilder();

            string strPart;
            int index = 0;

            while (index < str.Length)
            {
                if (index + length >= str.Length)
                {
                    strPart = str.Substring(index, str.Length - index - 1);
                    formatedString.Append(strPart);
                }
                else
                {
                    strPart = str.Substring(index, length - 1);
                    formatedString.Append(strPart + '-' + '\n');
                }

                index += length;
            }

            return formatedString.ToString();
        }

        private string FormatToBiggerLength(string str, int length)
        {
            return str + new String(' ', length - str.Length);
        }
    }
}
