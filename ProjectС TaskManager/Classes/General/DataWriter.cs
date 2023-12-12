using Newtonsoft.Json;
using ProjectB_TaskManager.Classes.MyTasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ProjectB_TaskManager.Classes.General
{
    public class ListDataWriter<T>
    {
        private readonly List<T> items;

        public ListDataWriter(List<T> items) 
        {
            this.items = items;
        }

        public bool WriteToJsonFile(string path)
        {
            try
            {
                string json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(path, json);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
