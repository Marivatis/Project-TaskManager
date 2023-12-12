using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;
using ProjectB_TaskManager.Classes.MyTasks;

namespace ProjectB_TaskManager.Classes.General
{
    public class ListDataReader
    {
        public static void ReadFromJsonFile<T>(string path, ref List<T> items)
        {
            try
            {
                string jsonFromFile = File.ReadAllText(path);

                JsonSerializerSettings settings = new JsonSerializerSettings { Converters = { new MyTaskConverter() } };
                items = JsonConvert.DeserializeObject<List<T>>(jsonFromFile, settings);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        
    }
}
