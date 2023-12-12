using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectB_TaskManager.Classes.Consoles
{
    public class TablePrinter
    {
        private readonly List<ITablePrintable> printableObjects;

        public TablePrinter(List<ITablePrintable> printableObjects)
        {
            this.printableObjects = printableObjects;
        }

        public void PrintTable()
        {
            if (printableObjects.Count == 0)
                return;

            string table = GetTable();

            Console.WriteLine(table);
        }

        public string GetTable()
        {
            StringBuilder table = new StringBuilder();

            table.AppendLine(printableObjects[0].GetTableFooter());
            table.AppendLine(printableObjects[0].GetTableHeader());
            table.AppendLine(printableObjects[0].GetTableFooter());

            foreach (ITablePrintable item in printableObjects)
            {
                table.AppendLine(item.GetTableRow());
                table.AppendLine(item.GetTableFooter());
            }

            return table.ToString();
        }
    }
}