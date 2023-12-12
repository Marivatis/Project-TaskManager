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

            mainProjectPath = GoUpDirectories(basePath, 4);
        }
        private string GoUpDirectories(string path, int levels)
        {
            string absolutePath = Path.GetFullPath(path);

            for (int i = 0; i < levels; i++)
            {
                absolutePath = Directory.GetParent(absolutePath)?.FullName ?? absolutePath;
            }

            return absolutePath;
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
