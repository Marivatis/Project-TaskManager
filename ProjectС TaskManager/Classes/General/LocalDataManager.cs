using ProjectС_TaskManager.Classes.General;
using ProjectС_TaskManager.Classes.MyTasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectС_TaskManager.Classes.General
{
    public class LocalDataManager
    {
        protected string mainProjectPath;

        public LocalDataManager()
        {
            GetMainProjectPath();
        }

        public string MianProjectPath => mainProjectPath;

        private void GetMainProjectPath()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

           mainProjectPath = Path.Combine("..", "..", "..");
        }

        public void SaveList(string path, List<MyTask> list)
        {
            ListDataWriter<MyTask> dataWriter = new ListDataWriter<MyTask>(list);

            dataWriter.WriteToJsonFile(path);
        }
        public void LoadList(string path, out List<MyTask> list)
        {
            list = new List<MyTask>();

            ListDataReader.ReadFromJsonFile(path, ref list);
        }
    }
}
