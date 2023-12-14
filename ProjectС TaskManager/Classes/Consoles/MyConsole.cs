using ProjectС_TaskManager.Classes.MyTasks;
using ProjectС_TaskManager.Enums;
using ProjectС_TaskManager.Classes.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ProjectС_TaskManager.Classes.Consoles
{
    public class MyConsole
    {
        private string tasksListPath;

        private LocalDataManager localDataManager;

        private MyTaskManager taskManager;

        public MyConsole()
        {
            LoadTasks();
        }

        public static event EventHandler ProgramStarted;

        public void Run()
        {
            ProgramStarted?.Invoke(this, EventArgs.Empty);

            RunMainMenu();
        }
        private void RunMainMenu()
        {
            Printer.PrintMainMenu();

            int option = int.MaxValue;

            while (option != 0)
            {
                option = MyConsoleReader.ReadInt32("Enter any main menu option --> ", 0, 8);

                MainMenuSwitch(option);
            }
        }

        private void LoadTasks()
        {
            localDataManager = new LocalDataManager();

            tasksListPath = Path.Combine(localDataManager.MianProjectPath, "Tasks", "Tasks.json");
            Console.WriteLine($"[Tasks saving file path is: {tasksListPath}]");

            localDataManager.LoadList(tasksListPath, out List<MyTask> tasks);

            taskManager = new MyTaskManager(tasks);
        }

        private void MainMenuSwitch(int mainMenuOption)
        {
            int option;

            switch (mainMenuOption)
            {
                case 0:
                    localDataManager.SaveList(tasksListPath, taskManager.ToList());

                    Console.WriteLine("Have a nice day!");
                    break;
                case 1: // Add task
                    Printer.PrintAddMenu();

                    option = MyConsoleReader.ReadInt32("Enter any adding option --> ", 1, 4);

                    AddTaskSwitch(option);
                    break;
                case 2: // Print 
                    PrintTasks(taskManager);
                    break;
                case 3: // Sort tasks by remaining date
                    taskManager.SortTasksByRemainingDate();
                    Console.WriteLine("Tasks has been successfully sorted.");
                    break;
                case 4: // Mark task as completed
                    MarkTaskAsCompleted();
                    break;
                case 5: // Delete task
                    RemoveTask();
                    break;
                case 6: // Clear task manager
                    taskManager.Clear();
                    Console.WriteLine("Task manager list has been successfully cleared.");
                    break;
                case 7: // Filter tasks
                    FilterTasks();
                    break;
                case 8: // Print main menu
                    Printer.PrintMainMenu();
                    break;
            }
        }

        private void AddTaskSwitch(int option)
        {
            switch (option)
            {
                case 1: // Add university task
                    AddUniversityTask();
                    break;
                case 2: // Add university task randomly
                    AddUniversityTasksRandomly();
                    break;
                case 3: // Add general task
                    AddGeneralTask();
                    break;
                case 4: // Add general task randomly
                    AddGeneralTasksRandomly();
                    break;
            }
        }

        private void AddUniversityTask()
        {
            try
            {
                MyUniversityTask task = new MyUniversityTask();

                task.Id = MyConsoleReader.ReadInt32("Enter task id [1, 99] --> ", 1, 99);

                PrintPossibleEnumValues<UniversityCourses>();
                task.CourseName = MyConsoleReader.ReadEnum<UniversityCourses>("Enter course name --> ");

                task.Description = MyConsoleReader.ReadString("Enter task description --> ");
                task.Deadline = MyConsoleReader.ReadDateTime("Enter task deadline [dd.mm.yyyy] --> ", "dd.MM.yyyy");

                PrintPossibleEnumValues<MyTaskStatus>();
                task.Status = MyConsoleReader.ReadEnum<MyTaskStatus>("Enter task status --> ");

                taskManager.Add(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddUniversityTask();
            }
        }
        private void AddUniversityTasksRandomly()
        {
            int count = MyConsoleReader.ReadInt32("Enter how many tasks you want to add --> ");

            MyTaskRandomProperties randomProperties = new MyTaskRandomProperties();

            while (count --> 0)
            {
                MyUniversityTask task = new MyUniversityTask();

                task.Id = randomProperties.GetId();
                task.CourseName = randomProperties.GetCourseName();
                task.Description = randomProperties.GetDescription();
                task.Deadline = randomProperties.GetDeadline();
                task.Status = randomProperties.GetTaskStatus();

                taskManager.Add(task);
            }
        }

        private void AddGeneralTask()
        {
            try
            {
                MyGeneralTask task = new MyGeneralTask();

                task.Id = MyConsoleReader.ReadInt32("Enter task id [1, 99] --> ", 1, 99);
                task.Title = MyConsoleReader.ReadString("Enter title --> ");
                task.Description = MyConsoleReader.ReadString("Enter task description --> ");
                task.Deadline = MyConsoleReader.ReadDateTime("Enter task deadline [dd.mm.yyyy] --> ", "dd.MM.yyyy");
                task.Status = MyConsoleReader.ReadEnum<MyTaskStatus>("Enter task status --> ");

                taskManager.Add(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddGeneralTask();
            }
        }
        private void AddGeneralTasksRandomly()
        {
            int count = MyConsoleReader.ReadInt32("Enter how many tasks you want to add --> ");

            MyTaskRandomProperties randomProperties = new MyTaskRandomProperties();

            while (count-- > 0)
            {
                MyGeneralTask task = new MyGeneralTask();

                task.Id = randomProperties.GetId();
                task.Title = randomProperties.GetTitle();
                task.Description = randomProperties.GetDescription();
                task.Deadline = randomProperties.GetDeadline();
                task.Status = randomProperties.GetTaskStatus();

                taskManager.Add(task);
            }
        }

        private void PrintTasks(MyTaskManager manager)
        {
            if (manager.Count == 0)
            {
                Console.WriteLine("Task manager list is empty.");
                return;
            }

            Console.WriteLine("Your university tasks:");
            PrintTasks(manager, typeof(MyUniversityTask));

            Console.WriteLine("Your general tasks:");
            PrintTasks(manager, typeof(MyGeneralTask));            
        }
        private void PrintTasks(MyTaskManager manager, Type type)
        {
            if (!manager.Any(task => type.IsAssignableFrom(task.GetType())))
            {
                return;
            }

            List<ITablePrintable> list = TablePrinter.ToTablePrintableList(manager.ToList(), type);
            TablePrinter tablePrinter = new TablePrinter(list);
            tablePrinter.PrintTable();
        }

        private void RemoveTask()
        {
            Type type = MyConsoleReader.ReadMyTaskType();
            PrintTasks(taskManager, type);

            int id = MyConsoleReader.ReadInt32("Enter task id you want to remove --> ", 1, 99);

            for (int i = 0; i < taskManager.Count; i++)
            {
                if (taskManager[i].GetType() == type && taskManager[i].Id == id)
                {
                    taskManager.RemoveAt(i);
                    Console.WriteLine("Task has been successfully removed.");
                    return;
                }
            }

            Console.WriteLine("No task whith this id was found.");
        }
        private void MarkTaskAsCompleted()
        {
            Type type = MyConsoleReader.ReadMyTaskType();
            PrintTasks(taskManager, type);

            int id = MyConsoleReader.ReadInt32("Enter task id you completed --> ", 1, 99);

            taskManager.MarkAsCompleted(id);
        }
        private void FilterTasks()
        {
            Printer.PrintFilterMenu();

            int option = MyConsoleReader.ReadInt32("Enter any filtrating option --> ", 1, 2);

            MyTaskManager taskManager = new MyTaskManager();

            switch (option)
            {
                case 1:
                    DateTime date = MyConsoleReader.ReadDateTime("Enter preferable date to filter [dd.mm.yyyy] --> ", "dd.MM.yyyy");

                    taskManager = this.taskManager.Filter(task => task.Deadline.Equals(date));
                    break;
                case 2:
                    PrintPossibleEnumValues<MyTaskStatus>();

                    MyTaskStatus status = MyConsoleReader.ReadEnum<MyTaskStatus>("Enter preferable task status to filter --> ");

                    taskManager = this.taskManager.Filter(task => task.Status.Equals(status));
                    break;
            }

            PrintTasks(taskManager);
        }

        private void PrintPossibleEnumValues<T>()
        {
            string enumValues = GetPossibleEnumValues<T>();

            Console.WriteLine(enumValues);
        }
        private string GetPossibleEnumValues<T>()
        {
            string[] enumValues = Enum.GetNames(typeof(T));

            string info = string.Join(", ", enumValues);

            return info;
        }
    }
}
